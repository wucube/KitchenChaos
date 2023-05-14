using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 盘子盛的食物的图标
/// </summary>
public class PlateIconsUI : MonoBehaviour
{

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }
    
    /// <summary>
    /// 更新盘子旁的食物图标
    /// </summary>
    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform =  Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);

            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
