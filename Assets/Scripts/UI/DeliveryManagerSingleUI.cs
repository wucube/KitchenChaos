using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 单个要交付的食谱UI
/// </summary>
public class DeliveryManagerSingleUI : MonoBehaviour
{
    /// <summary>
    /// 食谱名文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI recipeNameText;

    /// <summary>
    /// 食谱示例图片容器
    /// </summary>
    [SerializeField] private Transform iconContainer;

    /// <summary>
    /// 食谱示例图片
    /// </summary>
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    /// <summary>
    /// 根据菜谱设置UI文本与图片
    /// </summary>
    /// <param name="recipeSO"></param>
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;

            Destroy(child.gameObject);
        }
        
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }

    }
}
