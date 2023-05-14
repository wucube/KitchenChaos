using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 垃圾桶
/// </summary>
public class TrashCounter : BaseCounter
{
    /// <summary>
    /// 往垃圾桶丢东西时播放音效
    /// </summary>
    public static event EventHandler OnAnyObjectTrashed;
    
    /// <summary>
    /// 重置静态事件
    /// </summary>
    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }
    public override void Interact(Player player)
    {
        //若玩家持有炊事对象
        if (player.HasKitchenObject())
        {
            //销毁玩家手中的炊事对象
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
  