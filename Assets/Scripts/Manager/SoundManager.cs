using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 声音管理器
/// </summary>
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 玩家设定的声效音量
    /// </summary>
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    /// <summary>
    /// 音源引用配置
    /// </summary>
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    /// <summary>
    /// 垃圾桶_丢垃圾的事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.objectDrop, trashCounter.transform.position);
    }

    /// <summary>
    /// 基础操作台_放置对象事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    /// <summary>
    /// 玩家_拾取对象事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    /// <summary>
    /// 切菜操作台_切对象事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    /// <summary>
    /// 提交管理器_根据食谱提交失败事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, Camera.main.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, Camera.main.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)],position, volume);
    }
    /// <summary>
    /// 在世界空间的指定位置播放音源
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="position"></param>
    /// <param name="volumeMultiplier"></param>
    private void PlaySound(AudioClip audioClip,Vector3 position,float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }
    
    /// <summary>
    /// 播放脚本声
    /// </summary>
    /// <param name="position"></param>
    /// <param name="volume"></param>
    public void PlayFootstepsSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.warning, position);
    }
     
    /// <summary>
    /// 改变音量
    /// </summary>
    public void ChangedVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
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
