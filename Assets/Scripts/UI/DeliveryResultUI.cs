using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 交付结果UI
/// </summary>
public class DeliveryResultUI : MonoBehaviour
{
    /// <summary>
    /// 动画弹出
    /// </summary>
    private const string POPUP = "Popup";
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false);
    }
    
    /// <summary>
    /// 交付管理器_提交食谱失败事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED";
    }
    
    /// <summary>
    /// 交付管理器_提交食谱成功事件处理器
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        animator.SetTrigger(POPUP);
        gameObject.SetActive(true);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
    }
}
