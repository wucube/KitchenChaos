using UnityEngine;


/// <summary>
/// 炊事对象通用行为
/// </summary>
public interface IKitchenObjectParent
{
    /// <summary>
    /// 得到炊事对象跟随的Transform
    /// </summary>
    /// <returns></returns>
    public Transform GetKitchenObjectFollowTransform();
    
    /// <summary>
    /// 设置持有的炊事对象
    /// </summary>
    /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject);
    
    /// <summary>
    /// 得到持有的炊事对象
    /// </summary>
    /// <returns></returns>
    public KitchenObject GetKitchenObject();

    /// <summary>
    /// 清除持有的炊事对象
    /// </summary>
    public void ClearKitchenObject();

    /// <summary>
    /// 是否持有炊事对象
    /// </summary>
    /// <returns></returns>
    public bool HasKitchenObject();
}