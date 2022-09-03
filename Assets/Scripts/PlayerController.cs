using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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

    


       #region -INPUTS IN-

        private PlayerInputs playerInputs;

        public Vector2 input_Movement;
        public Vector2 input_View;

       #endregion


        #region -MOVEMENT STATS-            

        [Header("Movement Values")]
        public float walkingSpeed = 10f;
        public float runningSpeed = 15f;
        public float jumpSpeed = 3f;
        public float gravity = 10f; 
        public float viewClampYMin = -70;
        public float viewClampYMax = 80;       
        float rotationX = 0;
        public bool canMove = true;
        public bool runInterrupt = false;
        public bool isRunning;



        #endregion

    void Awake(){
        
    }


    // Start is called before the first frame update
    void Start()
    {

        // characterController = GetComponent<CharacterController>();
        

                // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible=false;    
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region -Funcs-

    public void init(PlayerManager initializer){

        playerManager = initializer;
        playerCap = playerManager.playerCap;
        playerController = playerManager.playerController;
        // leanPoint = playerManager.leanPoint;
        // playerHead = playerManager.playerHead;
        playerCam = playerManager.playerCam;
        // gunCam = playerManager.gunCam;   

        inventoryController = initializer.GetComponent<InventoryController>();



        // WeaponUI = playerManager.WeaponUI;
        // WUIC = playerManager.WUIC;
        // ScoreCard = playerManager.ScoreCard;
        // AmmoCounter = playerManager.AmmoCounter;
        // CST = playerManager.CST;
    
    }

    #endregion


}



