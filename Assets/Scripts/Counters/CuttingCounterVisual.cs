using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 切菜操作台高亮
/// </summary>
public class CuttingCounterVisual : MonoBehaviour
{
    private const string Cut = "Cut";
    
    [SerializeField] private CuttingCounter cuttingCounter;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    /// <summary>
    /// 切菜操作台-动画事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(Cut);
    }
    
}
