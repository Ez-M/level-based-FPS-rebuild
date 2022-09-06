using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static scr_Models;


[RequireComponent(typeof(CharacterController))]


public class PlayerController : MonoBehaviour
{

    #region -Init Connections-
    private bool gunisInit;

    private PlayerManager playerManager;
    private GameObject playerCap;
    private GameObject leanPoint;
    private GameObject playerHead;
    private Camera playerCam;
    private Camera gunCam;

    private PlayerController playerController;
    private PlayerInputHandler playerInputHandler;
    // private PlayerMovement playerMovement;

    private GameObject WeaponUI;
    private GameObject activeWeapon;

    private WeaponUIController WUIC;

    public InventoryController inventoryController;

    private TMPro.TextMeshProUGUI ScoreCard;
    private TMPro.TextMeshProUGUI AmmoCounter;
    private TMPro.TextMeshProUGUI CST; //centerscreentext

    private CharacterController characterController;

    #endregion

    Vector3 moveDirection = Vector3.zero;




    //    #region -INPUTS IN-

    //     private PlayerInputs playerInputs;

    //     public Vector2 input_Movement;
    //     public Vector2 input_View;

    //     public InputAction Jump;
    //     public InputAction Sprint;
    //     public InputAction Interact;
    //     public InputAction Fire1;
    //     public InputAction Fire2;
    //     public InputAction Fire3;
    //     public InputAction Reload;
    //     public InputAction Weapon1;
    //     public InputAction Weapon2;



    //    #endregion


    #region -MOVEMENT STATS-            

    [Header("Movement Values")]
    public PlayerSettingsModel playerSettings;
    public float walkingSpeed = 10f;
    public float runningSpeed = 15f;
    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;
    public float gravityAmount;
    public float gravityMin;
    public float playerGravity;
    public float lookSpeed = 1f;

    public float viewClampYMin = -70;
    public float viewClampYMax = 80;
    float rotationX = 0;
    public bool canMove = true;
    public bool runInterrupt = false;
    public bool isRunning;

    #endregion


    private Vector2 input_Movement;
    private Vector2 input_View;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;



    void Awake()
    {


    }




    // Start is called before the first frame update
    void Start()
    {

        // characterController = GetComponent<CharacterController>();


        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;



    }

    // Update is called once per frame
    void Update()
    {


        #region -Movement and Look- 
        if (canMove)
        {
            input_Movement = playerInputHandler.input_Movement;
            input_View = playerInputHandler.input_View;
            CalculateMovement();
            CalculateView();
            CalculateJumpChange();
        }

        #endregion

    }

    #region -isLook&isMove-
    public bool isLook
    {
        get
        {
            if (input_View != Vector2.zero)
                return true;
            else
                return false;
        }
    }
    public bool isMove
    {
        get
        {
            if (input_Movement != Vector2.zero)
                return true;
            else
                return false;
        }
    }

    #endregion

    #region -Funcs-

    public void init(PlayerManager initializer)
    {

        playerManager = initializer;
        playerCap = playerManager.playerCap;
        playerController = playerManager.playerController;
        // leanPoint = playerManager.leanPoint;
        playerHead = playerManager.playerHead;
        playerCam = playerManager.playerCam;
        // gunCam = playerManager.gunCam;   
        playerInputHandler = playerManager.playerInputHandler;

        // inventoryController = initializer.GetComponent<InventoryController>();
        characterController = initializer.characterController;

        newCameraRotation = playerHead.transform.localRotation.eulerAngles;
        newCharacterRotation = playerHead.transform.localRotation.eulerAngles;

        playerInputHandler.Jump.started += JumpFunction;
 

        // WeaponUI = playerManager.WeaponUI;
        // WUIC = playerManager.WUIC;
        // ScoreCard = playerManager.ScoreCard;
        // AmmoCounter = playerManager.AmmoCounter;
        // CST = playerManager.CST;

    }


    private void CalculateMovement()
    {


        float forwardSpeed = (isRunning == true ? runningSpeed : walkingSpeed) * input_Movement.y * Time.deltaTime;
        float horizontalSpeed = (isRunning == true ? runningSpeed : walkingSpeed) * input_Movement.x * Time.deltaTime;

        Vector3 newMovementSpeed = new Vector3(horizontalSpeed, 0, forwardSpeed);


        newMovementSpeed = transform.TransformDirection(newMovementSpeed);


        if (playerGravity > gravityMin)
        {
            playerGravity -= gravityAmount * Time.deltaTime;
        }
        else
        {

        }


        if (playerGravity < -0.1 && characterController.isGrounded)
        {
            playerGravity = -0.1f;
        }

        newMovementSpeed.y += playerGravity;
        newMovementSpeed += jumpingForce * Time.deltaTime;

        characterController.Move(newMovementSpeed);
    }




    private void CalculateView()
    {
        newCameraRotation.x += playerSettings.ViewYSensitivity * (playerSettings.ViewYInverted ? input_View.y : -input_View.y) * Time.deltaTime; // swapping axis mid line because we are referring firs to unity axis and then input axis which are flipped
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);

        newCharacterRotation.y += playerSettings.ViewXSensitivity * (playerSettings.ViewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);


        playerHead.transform.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateJumpChange()
    {

        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.JumpingFalloff);

    }

    private void JumpFunction(InputAction.CallbackContext value)
    {
        if (characterController.isGrounded)
        {
            //Jump
            jumpingForce = Vector3.up * playerSettings.JumpingHeight;
            playerGravity = 0;

        }



    }



    #endregion


}



