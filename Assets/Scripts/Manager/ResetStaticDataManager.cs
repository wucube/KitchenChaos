using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 重置静态数据管理器
/// </summary>
public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
