using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 烤炉的将要煎焦的闪烁UI
/// </summary>
public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        animator.SetBool(IS_FLASHING, false);
    }

    /// <summary>
    /// 烤炉操作台_对象处理进度改变事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        //显示快要煎焦UI的进度值
        float burnShowProgressAmount = 0.5f;

        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, show);
        
    }
}
