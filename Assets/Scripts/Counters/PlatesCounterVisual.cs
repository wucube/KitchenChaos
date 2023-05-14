using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盘子操作台的高亮
/// </summary>
public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    
    /// <summary>
    /// 盘子可视化对象列表
    /// </summary>
    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        plateCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    /// <summary>
    /// 盘子操作台_盘子移除事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        //移除盘子对象列表末尾的盘子
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    /// <summary>
    /// 盘子操作台_盘子生成事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = .1f;
        
        //盘子对象的Y轴值为 生成的盘子总数量 * Y轴偏移值
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
