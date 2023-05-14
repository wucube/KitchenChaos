using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炉子操作台视觉化
/// </summary>
public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    /// <summary>
    /// 炉子上的游戏对象
    /// </summary>
    [SerializeField] private GameObject stoveOnGameObject;
    /// <summary>
    /// 粒子对象
    /// </summary>
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }
    
    /// <summary>
    /// 炉子操作台_油煎对象状态改变的事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        //油煎对象为正在油煎或油煎完成，就显示炉子视觉对象
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
