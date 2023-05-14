using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 菜谱配置
/// </summary>
[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{ 
    /// <summary>
    /// 食物对象配置列表
    /// </summary>
    public List<KitchenObjectSO> kitchenObjectSOList;
    
    /// <summary>
    /// 菜谱名
    /// </summary>
    public string recipeName;
}
