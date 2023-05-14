using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盘子对象视觉化
/// </summary>
public class PlateCompleteVisual : MonoBehaviour
{
    /// <summary>
    /// 炊事对象配置的游戏对象
    /// </summary>
    [Serializable ]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    
    /// <summary>
    /// 盘子对象
    /// </summary>
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    /// <summary>
    /// 炊事对象配置列表
    /// </summary>
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        
        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSoGameObject.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// 盘子对象的盛烹饪食物的事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        //遍历盘子的炊事对象列表
        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSoGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSoGameObject.gameObject.SetActive(true);
            }
        }
    }
}
