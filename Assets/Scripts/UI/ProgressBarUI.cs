using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 进度条UI
/// </summary>
public class ProgressBarUI : MonoBehaviour
{
    /// <summary>
    /// 有加工进度的游戏对象
    /// </summary>
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;
    
    /// <summary>
    /// 有处理进度
    /// </summary>
    private IHasProgress hasProgress;

    private void Start()
    {
        //获取有加工进度对象的脚本
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError("Game Object" + hasProgressGameObject + "does not have a component that implements IHasProgress!");
        }
        
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        
        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
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
