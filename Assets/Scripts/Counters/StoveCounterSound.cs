using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炉子操作台的声音
/// </summary>
public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;

    /// <summary>
    /// 警告声计时器
    /// </summary>
    private float warningSoundTimer;

    /// <summary>
    /// 是否播放警告声
    /// </summary>
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }
    
    /// <summary>
    /// 油煎对象_油煎进度改变事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        //显示警告的进度值
        float burnShowProgressAmount = 0.5f;

        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }
    
    /// <summary>
    /// 油煎对象_油煎对象状态改变事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        //油煎对象的状态为正在油煎或油煎完成，就播放声音
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (playSound)
            audioSource.Play();
        else
            audioSource.Pause();
    }

    private void Update()
    {
        //若播放警告声
        if (playWarningSound) 
        { 
            warningSoundTimer -= Time.deltaTime;

            if (warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = 0.2f;
                warningSoundTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
