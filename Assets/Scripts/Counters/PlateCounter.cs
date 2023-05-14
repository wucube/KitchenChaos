using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盘子操作台
/// </summary>
public class PlateCounter : BaseCounter
{
    /// <summary>
    /// 盘子生成事件
    /// </summary>
    public event EventHandler OnPlateSpawned;
    /// <summary>
    /// 盘子移除事件
    /// </summary>
    public event EventHandler OnPlateRemoved;
    
    /// <summary>
    /// 盘子对象配置
    /// </summary>
    [SerializeField] private KitchenObjectSO plateKitchenObjectS0;
    
    /// <summary>
    /// 盘子生成的计时器
    /// </summary>
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    /// <summary>
    /// 盘子生成的数量
    /// </summary>
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;

        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (KitchenGameManager.Instance.IsGamePlaying() && platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        //如果玩家空手
        if (!player.HasKitchenObject())
        {
            //如果盘子的生成数量大于0
            if (platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;
                //将盘子生成到玩家手中
                KitchenObject.SpawnKitchenObject(plateKitchenObjectS0, player);
                OnPlateRemoved?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}
