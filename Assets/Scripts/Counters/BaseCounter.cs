
using UnityEngine;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// 基础操作台
/// </summary>
public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    /// <summary>
    /// 往桌子放置东西时播放音效
    /// </summary>
    public static event EventHandler OnAnyObjectPlacedHere;
    
    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    
    /// <summary>
    /// 操作台上方的点
    /// </summary>
    [SerializeField] private Transform counterTopPoint;
   
    /// <summary>
    /// 操作台上放置的炊事对象
    /// </summary>
    private KitchenObject kitchenObject;

    /// <summary>
    /// 玩家与操作台的交互函数
    /// </summary>
    /// <param name="player"></param>
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    /// <summary>
    /// 玩家与操作台的第二交互函数
    /// </summary>
    /// <param name="player"></param>
    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }

    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }

    }
    
    public  KitchenObject GetKitchenObject() => kitchenObject;

    public void ClearKitchenObject() => kitchenObject = null;

    public bool HasKitchenObject() => kitchenObject != null;
}
