using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 交付操作台
/// </summary>
public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            //若玩家拿着盘子
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
