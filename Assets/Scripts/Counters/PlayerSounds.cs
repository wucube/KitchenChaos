using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
/// <summary>
/// 玩家的声效
/// </summary>
public class PlayerSounds : MonoBehaviour
{
    private Player player;
    /// <summary>
    /// 脚步计时器
    /// </summary>
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;

        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
            //若玩家正在行走
            if (player.IsWalking()) { 
                float volume = 2f;
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
        }
    }
}
