using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景加载器
/// </summary>
public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    private static Scene targetScene;

    /// <summary>
    /// 场景加载
    /// </summary>
    /// <param name="targetScene"></param>
    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        //加载到空场景
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    /// <summary>
    /// 加载目标场景的回调
    /// </summary>
    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
