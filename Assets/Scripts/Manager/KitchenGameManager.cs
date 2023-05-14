using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 游戏管理器
/// </summary>
public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    
    /// <summary>
    /// 游戏状态变化事件
    /// </summary>
    public event EventHandler OnStateChanged;
    /// <summary>
    /// 游戏暂停事件
    /// </summary>
    public event EventHandler OnGamePaused;
    /// <summary>
    /// 游戏结束暂停事件
    /// </summary>
    public event EventHandler OnGameUnpaused;

    /// <summary>
    /// 游戏运行状态
    /// </summary>
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    /// <summary>
    /// 游戏状态
    /// </summary>
    private State state;

    /// <summary>
    /// 倒计时开始计时器
    /// </summary>
    private float countdownToStartTimer = 5f;

    /// <summary>
    /// 游玩时间计时器
    /// </summary>
    private float gamePlayingTimer;
    
    /// <summary>
    /// 游玩时间计时器最大值
    /// </summary>
    private float gamePlayingTimerMax = 30f;

    /// <summary>
    /// 游戏是否暂停
    /// </summary>
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    /// <summary>
    /// 游戏输入互动事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;

            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.CountdownToStart:
                //倒计时开始
                countdownToStartTimer -= Time.deltaTime;
                
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                //游玩倒计时
                gamePlayingTimer -= Time.deltaTime;

                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;
        }
    }

    /// <summary>
    /// 游戏输入暂停事件处理函数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }
    
    /// <summary>
    /// 游戏暂停与恢复的切换
    /// </summary>
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
           Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
    
    /// <summary>
    /// 游戏是否处于游玩状态
    /// </summary>
    /// <returns></returns>
    public bool IsGamePlaying() => state == State.GamePlaying;
    
    /// <summary>
    /// 倒计时开始是否激活
    /// </summary>
    /// <returns></returns>
    public bool IsCountdownToStartActive() => state == State.CountdownToStart;
    
    /// <summary>
    /// 获取倒计时开始的计时器
    /// </summary>
    /// <returns></returns>
    public float GetCountdownToStartTimer() => countdownToStartTimer;

    public bool IsGameOver() => state == State.GameOver;
    
    /// <summary>
    /// 获取 剩余游玩时间 归一化
    /// </summary>
    /// <returns></returns>
    public float GetGamePlayingTimerNormalized() => 1 - (gamePlayingTimer / gamePlayingTimerMax);
}
