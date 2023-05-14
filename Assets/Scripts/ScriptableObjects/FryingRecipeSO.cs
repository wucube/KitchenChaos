using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 油煎食谱配置
/// </summary>
[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;

    public KitchenObjectSO output;
    
    /// <summary>
    /// 油煎计时最大值
    /// </summary>
    public float fryingTimerMax;
}
