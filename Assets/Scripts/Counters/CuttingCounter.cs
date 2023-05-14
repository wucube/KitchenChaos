using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 切菜的操作台
/// </summary>
public class CuttingCounter : BaseCounter, IHasProgress
{
    /// <summary>
    /// 切菜事件-播放音效
    /// </summary>
    public static event EventHandler OnAnyCut;

    /// <summary>
    /// 重置静态事件成员 
    /// </summary>
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    public event EventHandler <IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    /// <summary>
    /// 切菜事件-播放动画
    /// </summary>
    public event EventHandler OnCut;
    
    /// <summary>
    /// 切菜食谱配置表
    /// </summary>
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    
    /// <summary>
    /// 切菜进度
    /// </summary>
    private int cuttingProgress;

    public override void Interact(Player player)
    {
        //如果切菜操作台没持有对象
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Player carrying something that can be Cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    //切菜食谱配置表
                    CuttingRecipeSO cuttingRecipeSO =  GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
            else
            {
                //Player not carrying anything
            }
        }
        //如果切菜操作台放置了对象
        else
        {
            if (player.HasKitchenObject())
            {
                //若玩家持有盘子对象
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //尝试往盘子中添加食物
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            //若玩家没有持有炊事对象
            else
            {
                //将切菜对象交由玩家
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        //若放置的对象为加工原料
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //切菜进度增加
            cuttingProgress++;
            
            OnCut?.Invoke(this,EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            
            CuttingRecipeSO cuttingRecipeSO =  GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });
            
            //若切菜进度大于切菜配置的最大进度
            if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                //得到加工后的原料
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                //销毁加工前的原料
                GetKitchenObject().DestroySelf();
                //生成新的炊事对象
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }
    
    /// <summary>
    /// 放置的是否为食谱原料
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        
        return cuttingRecipeSO != null;
    }
    
    /// <summary>
    /// 得到加工后的原料
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO =  GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    
    /// <summary>
    /// 根据放置的原料得到对应切菜的配置
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
