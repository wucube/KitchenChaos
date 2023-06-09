using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 交付管理器
/// </summary>
public class DeliveryManager : MonoBehaviour
{
    /// <summary>
    /// 食谱生成事件
    /// </summary>
    public event EventHandler OnRecipeSpawned;
    /// <summary>
    /// 完成食谱事件
    /// </summary>
    public event EventHandler OnRecipeCompleted;
    /// <summary>
    /// 根据食谱提交成功事件
    /// </summary>
    public event EventHandler OnRecipeSuccess;
    /// <summary>
    /// 根据食谱提交失败事件
    /// </summary>
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    
    /// <summary>
    /// 菜谱配置列表
    /// </summary>
    [SerializeField] private RecipeListSO recipeListSO;

    /// <summary>
    /// 待提交的菜谱列表
    /// </summary>
    private List<RecipeSO> waitingRecipeSOList;
    
    /// <summary>
    /// 生成菜谱计时器
    /// </summary>
    private float spawnRecipeTimer = 0f;
    private float spawnRecipeTimerMax = 4f;
    /// <summary>
    /// 待出菜谱数量最大值
    /// </summary>
    private int waitingRecipesMax = 4;

    /// <summary>
    /// 成功交付食物的数量
    /// </summary>
    private int successfulRecipesAmount;

    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        //若生成菜谱时间为0
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            //若游戏处于游玩状态 且 等待的菜谱列表数量小于最大值
            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax)
            {
                //随机出一个菜谱
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    
    /// <summary>
    /// 交付食谱对应的食物
    /// </summary>
    /// <param name="plateKitchenObject"></param>
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        //遍历待提交的食谱列表
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            //取出一道食谱
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            //判断食谱的食物对象数目 是否与 提交的盘子中的食物数量相等
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    // Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // Cycing through all ingredients in the Plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // Ingretient matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //This Recipe ingredient was not found on thee Plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    // Player delivered the correct recipe;

                    successfulRecipesAmount++;

                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // No matches found!
        // Player did not deliver a correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
    
    /// <summary>
    /// 得到待提交的食谱列表
    /// </summary>
    /// <returns></returns>
    public List<RecipeSO> GetWaitingRecipeSOList() => waitingRecipeSOList;
    
    /// <summary>
    /// 得到成功交付食谱的次数
    /// </summary>
    /// <returns></returns>
    public int GetSuccessfulRecipesAmount() => successfulRecipesAmount;
 
}

