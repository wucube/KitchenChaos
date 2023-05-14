using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

/// <summary>
/// 游戏开始倒计时UI
/// </summary>
public class GameStartCountdownUI : MonoBehaviour
{
    /// <summary>
    /// 数字弹出
    /// </summary>
    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    
    /// <summary>
    /// 上一个倒计时数字
    /// </summary>
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }
    
    /// <summary>
    /// 游戏管理器_游戏状态改变事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
            Hide();
    }

    private void Update()
    {
        //返回 大于或等于倒计时浮点数的最小整数
        int countdownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();
        
        //若上一个倒计时整数 不等于 当前倒计时整数
        if (previousCountdownNumber != countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);

            SoundManager.Instance.PlayCountdownSound();
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
