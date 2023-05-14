using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 切菜食谱配置
/// </summary>
[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    /// <summary>
    /// 输入的原料
    /// </summary>
    public KitchenObjectSO input;
    
    /// <summary>
    /// 输出的原料
    /// </summary>
    public KitchenObjectSO output;
    
    /// <summary>
    /// 切菜处理最大值
    /// </summary>
    public int cuttingProgressMax;
}
