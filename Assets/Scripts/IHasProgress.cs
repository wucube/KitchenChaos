using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理进度
/// </summary>
public interface IHasProgress
{

    /// <summary>
    /// 进度已经改变的事件
    /// </summary>
    public event EventHandler <OnProgressChangedEventArgs> OnProgressChanged;

    /// <summary>
    /// 进度改变的事件参数
    /// </summary>
    public class OnProgressChangedEventArgs:EventArgs
    {
        /// <summary>
        /// 食物处理进度归一化
        /// </summary>
        public float progressNormalized;
    }
}
