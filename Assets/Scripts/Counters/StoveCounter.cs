using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炉子操作台
/// </summary>
public class StoveCounter : BaseCounter, IHasProgress
{
    /// <summary>
    /// 油煎进度改变事件
    /// </summary>
    public event EventHandler <IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    /// <summary>
    /// 油煎对象状态改变事件
    /// </summary>
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    /// <summary>
    /// 油煎对象状态改变事件参数
    /// </summary>
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    /// <summary>
    /// 油煎对象的状态
    /// </summary>
    public enum State
    {
        Idle,
        //正在油煎
        Frying,
        //油煎完成
        Fried,
        //煎焦
        Burned,
    }

    /// <summary>
    /// 油煎食谱配置阵列
    /// </summary>
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    /// <summary>
    /// 煎焦对象配置阵列
    /// </summary>
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    
    /// <summary>
    /// 油煎对象状态
    /// </summary>
    private State state;

    /// <summary>
    /// 油煎计时
    /// </summary>
    private float fryingTimer;

    /// <summary>
    /// 油煎食谱配置
    /// </summary>
    private FryingRecipeSO fryingRecipeSO;

    /// <summary>
    /// 煎焦倒计时
    /// </summary>
    private float burningTimer;
    /// <summary>
    /// 煎焦食谱配置
    /// </summary>
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        //若炉子上放置着炊事对象
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:

                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    
                    //油煎完成
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    }
                    break;

                case State.Fried:
                    burningTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });
                
                    //煎焦
                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                    
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        //若烤炉上没有对象
        if (!HasKitchenObject())
        {
            //玩家持有炊事对象
            if (player.HasKitchenObject())
            {
                //若玩家的炊事对象能烤制
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //玩家将对象放置到烤炉上
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
                    {
                        progressNormalized = fryingTimer/fryingRecipeSO.fryingTimerMax
                    });
                }
                
            }
            else
            {
                //Player not carrying anything
            }
        }
        //若烤炉上有对象
        else
        {
            //若玩家持有炊事对象
            if (player.HasKitchenObject())
            {
                //若玩家拿着盘子
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //将烤制的食物装到盘子里
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            //若玩家没持有炊事对象
            else
            {
                //将烤炉上的对象给玩家
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
                {
                    progressNormalized = 0f
                });
            }
        }
    }
    
    /// <summary>
    /// 玩家将放置的对象能否油煎
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }
    
    /// <summary>
    /// 根据输入的食物得到对应食谱配置
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO =  GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    
    /// <summary>
    /// 根据输入的食物得到油煎食谱配置
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        //遍历油煎食谱阵列
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            //若油煎食谱中的输入 等于 传入的炊事对象
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    
    /// <summary>
    /// 根据输入的食物得到煎焦食谱配置
    /// </summary>
    /// <param name="inputKitchenObjectSO"></param>
    /// <returns></returns>
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            //若煎焦食谱中的输入 等于 传入的炊事对象
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

    /// <summary>
    /// 油煎是否完成
    /// </summary>
    /// <returns></returns>
    public bool IsFried()
    {
        return state == State.Fried;
    }
}
