using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 取出食材操作台的高亮化
/// </summary>
public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    
    /// <summary>
    /// 取出食材的操作台
    /// </summary>
    [SerializeField] private ContainerCounter containerCounter;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    /// <summary>
    /// 玩家从操作台取出食材的事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }

    //新增的取消订阅事件
    private void OnDestroy() 
    {
        containerCounter.OnPlayerGrabbedObject -= ContainerCounter_OnPlayerGrabbedObject;
    }
}
