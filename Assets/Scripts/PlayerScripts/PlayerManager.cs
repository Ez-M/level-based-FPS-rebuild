using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{


#region -Init Connections-
    public GameObject playerCap;
    public GameObject leanPoint;
    public GameObject playerHead;
    public Camera playerCam;

    public PlayerInputHandler playerInputHandler;

    public Camera gunCam;    

    public PlayerController playerController; 

    public GameObject WeaponUI;
    public GameObject activeWeapon;

    public WeaponUIController WUIC;

    public InventoryController inventoryController;

    public TMPro.TextMeshProUGUI ScoreCard;
    public TMPro.TextMeshProUGUI AmmoCounter;
    public TMPro.TextMeshProUGUI CST; //centerscreentext

    public CharacterController characterController;
   
    #endregion

    public float score;

    private float playerHP;

    void Awake(){
        init();
        playerInputHandler.init();
        playerController.init(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per framess
    void Update()
    {
        
    }

    #region -Funcs-
    public void init(){
        playerCap = gameObject.transform.GetChild(0).gameObject;
        playerController = playerCap.GetComponent<PlayerController>();
        // leanPoint = playerCap.transform.GetChild(0).gameObject;
        playerHead = playerCap.transform.GetChild(0).gameObject;
        // playerCam = playerHead.transform.GetChild(0).gameObject.GetComponent<Camera>();
        playerCam = playerCap.transform.GetChild(0).gameObject.GetComponent<Camera>();
        // gunCam = playerCam.transform.GetChild(0).gameObject.GetComponent<Camera>();

        playerInputHandler = playerCap.GetComponent<PlayerInputHandler>();

        characterController = playerCap.GetComponent<CharacterController>();
        // inventoryController = gameObject.GetComponent<InventoryController>();
        }

    public void setPlayerHP(float to)
    {
        playerHP = to;
    }

    public float updatePlayerHP(bool hurt, float strength)
    {
        if (hurt == true)
        {
            playerHP -= strength;
            Debug.Log("Player Hurt! HP: "+ playerHP);
            return playerHP;

        } else
        {
            playerHP += strength;
            Debug.Log("Player Heal! HP " + playerHP);
            return playerHP;
        }
    }

    public void scoreAdd(float input)
    {
        score += input;

        updateScore();
    }

    public void updateScore()
    {    
        ScoreCard.text = score.ToString();
    }

    #endregion


}
