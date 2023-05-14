using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 烧烤菜谱配置
/// </summary>
[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    
    /// <summary>
    /// 输入的炊事对象SO
    /// </summary>
    public KitchenObjectSO input;

    /// <summary>
    /// 输出的炊事对象SO
    /// </summary>
    public KitchenObjectSO output;
    
    /// <summary>
    /// 烧烤时间最大值
    /// </summary>
    public float burningTimerMax;
}
