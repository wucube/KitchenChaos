using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 取出食材的操作台
/// </summary>
public class ContainerCounter : BaseCounter
{
    /// <summary>
    /// 取出食材的事件
    /// </summary>
    public event EventHandler OnPlayerGrabbedObject;
    
    /// <summary>
    /// 食材配置信息
    /// </summary>
    /// <param name="player"></param>
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact( Player player)
    {
        //玩家没有携带任何东西
        if(!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            
            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
        }
    }
}
