using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    /// <summary>
    /// �в�
    /// </summary>
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footstep;
    /// <summary>
    /// �����Ϸ�
    /// </summary>
    public AudioClip[] objectDrop; 
    /// <summary>
    /// ����ʰȡ
    /// </summary>
    public AudioClip[] objectPickup; 
    /// <summary>
    /// ¯�ӆ����
    /// </summary>
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
