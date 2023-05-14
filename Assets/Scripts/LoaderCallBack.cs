using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空场景下的场景加载
/// </summary>
public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;

            Loader.LoaderCallBack();
        }
    }
}
