using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏暂停UI
/// </summary>
public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }

    private void Start()
    { 
        KitchenGameManager.Instance.OnGamePaused += KitchenGameManager_OnGamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUpaused;

        Hide();
    }
    
    /// <summary>
    /// 游戏管理器_结束暂停事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void KitchenGameManager_OnGameUpaused(object sender, EventArgs e)
    {
        Hide();
    }
    
    /// <summary>
    /// 游戏管理器_暂停事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void KitchenGameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
