using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盘子对象
/// </summary>
public class PlateKitchenObject : KitchenObject
{
    /// <summary>
    /// 烹饪材料添加事件
    /// </summary>
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    /// <summary>
    /// 烹饪材料添加事件参数
    /// </summary>
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    
    /// <summary>
    /// 有效炊事对象配置的列表
    /// </summary>
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    /// <summary>
    /// 当前炊事对象配置的列表
    /// </summary>
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    /// <summary>
    /// 尝试盛烹饪出的食物
    /// </summary>
    /// <param name="kitchenObjectSO">食物的配置信息</param>
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            // Not a valid ingredient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already has this type
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs(){ kitchenObjectSO = kitchenObjectSO } );
            
            return true;
        }
    }

    /// <summary>
    /// 获取炊事对象配置的列表
    /// </summary>
    /// <returns></returns>
    public List<KitchenObjectSO> GetKitchenObjectSOList() => kitchenObjectSOList;
}
