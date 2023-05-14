 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 盘子里食物的单个图标
/// </summary>
public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    
    /// <summary>
    /// 设置盘子中的食物图标
    /// </summary>
    /// <param name="kitchenObjectSO"></param>
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
