using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 要交付的食谱UI
/// </summary>
public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

        UpdateVisual();
    }
    
    /// <summary>
    /// 交付管理器_食谱生成事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    
    /// <summary>
    /// 交付管理器_食谱完成事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    
    /// <summary>
    /// 更新要交付的食谱UI
    /// </summary>
    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
      
    }
}
