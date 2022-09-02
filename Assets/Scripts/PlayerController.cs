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

    // private Transform gunEnd;   
    // public GameObject gun, hipPosition, ADSPosition;
    

    private CharacterController characterController;
   
    #endregion



    // Start is called before the first frame update
    void Start()
    {
                // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible=false;    


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
