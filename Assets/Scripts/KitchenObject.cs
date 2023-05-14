using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炊事对象
/// </summary>
public class KitchenObject : MonoBehaviour
{
    /// <summary>
    /// 炊事对象配置
    /// </summary>
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// 炊事对象的父对象
    /// </summary>
    private IKitchenObjectParent kitchenObjectParent;

    /// <summary>
    /// 得到炊事对象的配置信息
    /// </summary>
    /// <returns></returns>
    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;

    /// <summary>
    /// 设置炊事对象的父对象
    /// </summary>
    /// <param name="kitchenObjectParent"></param>
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        
        this.kitchenObjectParent = kitchenObjectParent;

        if (this.kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("kitchenObjectParent already has a KitchenObject!");
        }
        
        //炊事父对象将 该炊事对象 设为自己的持有对象
        kitchenObjectParent.SetKitchenObject(this);
        //炊事对象的父级transform为炊事父对象持有的专属子节点位置
        transform.parent = this.kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() => kitchenObjectParent;

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    /// <summary>
    /// 尝试获取盘子
    /// </summary>
    /// <param name="plateKitchenObject"></param>
    /// <returns></returns>
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        //若该炊事对象为盘子对象
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        //若该炊事对象不为盘子对象
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
    /// <summary>
    /// 生成炊事对象
    /// </summary>
    /// <param name="kitchenObjectSO">炊事对象SO</param>
    /// <param name="kitchenObjectParent">炊事父对象</param>
    /// <returns></returns>
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        //实例化炊事对象
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        //得到炊事对象的脚本
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        //设置炊事对象的父对象
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
