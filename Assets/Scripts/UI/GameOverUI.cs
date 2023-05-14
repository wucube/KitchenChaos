using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 游戏结束UI
/// </summary>
public class GameOverUI : MonoBehaviour
{ 
    /// <summary>
    /// 显示成功提交食谱的次数
    /// </summary>
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }
    
    /// <summary>
    /// 游戏状态变化事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        //若游戏结束
        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        }
        
        else
            Hide();
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
