using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 放置拾取炊事对象的操作台
/// </summary>
public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact( Player player)
    {
        //若操作台上没有炊事对象
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                
            }
        }
        //若操作台上有炊事对象
        else
        {
            //若玩家持有炊事对象
            if (player.HasKitchenObject())
            {
                //若玩家持有盘子 Player is holding a Plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //尝试用盘子装烹饪的食物
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                //若玩家没持有盘子
                else
                {   //若操作台上放着盘子
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //尝试用盘子装烹饪的食物
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            //若玩家空手
            else
            {
                //玩家带走操作台上的炊事对象
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    
}
