using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音效引用SO
/// </summary>
[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    /// <summary>
    /// 切菜
    /// </summary>
    public AudioClip[] chop;
    /// <summary>
    /// 交付失败
    /// </summary>
    public AudioClip[] deliveryFail;

    /// <summary>
    /// 交付成功
    /// </summary>
    public AudioClip[] deliverySuccess;

    /// <summary>
    /// 脚本声
    /// </summary>
    public AudioClip[] footstep;
    /// <summary>
    /// 对象放置
    /// </summary>
    public AudioClip[] objectDrop; 
    /// <summary>
    /// 对象拾取
    /// </summary>
    public AudioClip[] objectPickup; 
    /// <summary>
    /// 炉子嗞嗞响
    /// </summary>
    public AudioClip stoveSizzle;

    /// <summary>
    /// trash
    /// </summary>
    public AudioClip[] trash;

    /// <summary>
    /// 警告
    /// </summary>
    public AudioClip[] warning;
}
