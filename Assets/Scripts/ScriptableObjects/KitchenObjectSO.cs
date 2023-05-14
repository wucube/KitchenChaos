using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炊事对象的配置信息
/// </summary>
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    /// <summary>
    /// 炊事对象预制体
    /// </summary>
    public Transform prefab;
    /// <summary>
    /// 炊事对象精灵图
    /// </summary>
    public Sprite sprite;
    /// <summary>
    /// 炊事对象名称
    /// </summary>
    public string objectName;
}
