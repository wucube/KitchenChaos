using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音乐管理器
/// </summary>
public class MusicManager : MonoBehaviour
{
    /// <summary>
    /// 预设置音量的大小
    /// </summary>
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    
    /// <summary>
    /// 音乐音量
    /// </summary>
    private float volume = .3f;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
        audioSource.volume = volume;
    }

    /// <summary>
    /// 改变音量
    /// </summary>
    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    
    /// <summary>
    /// 获取音量
    /// </summary>
    /// <returns></returns>
    public float GetVolume()
    {
        return volume;
    }

}
