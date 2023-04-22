using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    /// <summary>
    /// 切菜
    /// </summary>
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footstep;
    /// <summary>
    /// 对象拖放
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
    public AudioClip[] trash;
    public AudioClip[] warning;
}
