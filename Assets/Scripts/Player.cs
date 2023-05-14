using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
/// <summary>
/// 玩家类
/// </summary>
public class Player : MonoBehaviour,IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    /// <summary>
    /// 拾取时播放音效
    /// </summary>
    public event EventHandler OnPickedSomething;
    
    /// <summary>
    /// 选中的操作台改变时的事件
    /// </summary>
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    /// <summary>
    /// 选中的操作台改变时的事件参数
    /// </summary>
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 选择的操作台
        /// </summary>
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private float movdSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    /// <summary>
    /// 操作台碰撞层级
    /// </summary>
    [SerializeField] private LayerMask countersLayerMask; 

    /// <summary>
    /// 炊事对象持有的位置
    /// </summary>
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private bool isWalking;

    /// <summary>
    /// 最近一次的互动方向
    /// </summary>
    private Vector3 lastInteractDir;

    /// <summary>
    /// 玩家选中的操作台
    /// </summary>
    private BaseCounter selectedCounter;

    /// <summary>
    /// 玩家持有的炊事对象
    /// </summary>
    private KitchenObject kitchenObject;
    

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }
    

    /// <summary>
    /// 互动事件处理函数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
    
    /// <summary>
    /// 第二互动事件处理函数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    /// <summary>
    /// 玩家是否存在行走
    /// </summary>
    /// <returns></returns>
    public bool IsWalking() => isWalking;

    /// <summary>
    /// 处理玩家与操作台的互动
    /// </summary>
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    
    /// <summary>
    /// 处理玩家移动
    /// </summary>
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = movdSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = 0.7f;
        
        //向移动方向投射胶囊体判断能否向前移动
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (!canMove) //不能朝前方移动
        {
            //尝试水平移动
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            //InputActions 死区输入设置为0.5f。水平输入向量超过0.5f 才能水平移动
            canMove = (moveDir.x < -0.5f || moveDir.x > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);

            //可以水平移动
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else 
            {
                //尝试垂直移动
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                //垂直输入向量超过0.5f 才能垂直移动
                canMove = (moveDir.z < -0.5f || moveDir.z > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //不能朝任何方向移动
                }
            }
        }
        
        //玩家移动
        if (canMove)
            transform.position += moveDir * moveDistance;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        //玩家逐步旋转至面朝移动方向
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    
    /// <summary>
    /// 设置选中玩家靠近的操作台
    /// </summary>
    /// <param name="selectedCounter"></param>
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this,new OnSelectedCounterChangedEventArgs(){selectedCounter = selectedCounter});
    }
    
    public Transform GetKitchenObjectFollowTransform() => kitchenObjectHoldPoint;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }
    
    public  KitchenObject GetKitchenObject() => kitchenObject;

    public void ClearKitchenObject() => kitchenObject = null;

    public bool HasKitchenObject() => kitchenObject != null;
}
