using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 烤炉煎焦警告UI
/// </summary>
public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;

        Hide();
    }
    
    /// <summary>
    /// 烤炉操作台_对象处理进度改变事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        //煎焦显示的进度值
        float burnShowProgressAmount = 0.5f;

        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
