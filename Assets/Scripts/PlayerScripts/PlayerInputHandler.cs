using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{


    #region -INPUTS IN-

    private PlayerInputs playerInputs;

    public Vector2 input_Movement;
    public Vector2 input_View;

    public InputAction Jump;
    public InputAction Sprint;
    public InputAction Interact;
    public InputAction Fire1;
    public InputAction Fire2;
    public InputAction Fire3;
    public InputAction Reload;
    public InputAction Weapon1;
    public InputAction Weapon2;

    public bool input_Jump;
    public bool input_Sprint;
    public bool input_Interact;
    public bool input_Fire1;
    public bool input_Fire2;
    public bool input_Fire3;
    public bool input_Reload;
    public bool input_A1;
    public bool input_A2;


    #endregion

    private bool AcceptingInputs;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init()
    {
        playerInputs = new PlayerInputs();

        Jump = playerInputs.Character.Jump;
        Sprint = playerInputs.Character.Sprint;
        Interact = playerInputs.Character.Interact;
        Fire1 = playerInputs.Weapons.Fire1;
        Fire2 = playerInputs.Weapons.Fire2;
        Fire3 = playerInputs.Weapons.Fire3;
        Reload = playerInputs.Weapons.Reload;

        playerInputs.Character.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        playerInputs.Character.View.performed += e => input_View = e.ReadValue<Vector2>();


        playerInputs.Enable();

    }


    #region  -InputFunctions-
    private void jumpisPressed(InputAction.CallbackContext value)
    {
        input_Jump = true;
    }

    private void jumpisReleased(InputAction.CallbackContext value)
    {
        input_Jump = false;
    }

    private void sprintisPressed(InputAction.CallbackContext value)
    {
        input_Sprint = true;
    }

    private void SprintisReleased(InputAction.CallbackContext value)
    {
        input_Sprint = false;
    }

    private void ReloadisPressed(InputAction.CallbackContext value)
    {
        input_Reload = true;

    }

    private void ReloadisReleased(InputAction.CallbackContext value)
    {
        input_Reload = false;

    }


    public void Fire1isPressed(InputAction.CallbackContext value)
    {
        input_Fire1 = true;

    }

    private void Fire1isReleased(InputAction.CallbackContext value)
    {
        input_Fire1 = false;
        // playerManager.activeShoot.hasFired = false;
    }


    private void Fire2isPressed(InputAction.CallbackContext value)
    {
        input_Fire2 = true;
    }

    private void Fire2isReleased(InputAction.CallbackContext value)
    {
        input_Fire2 = false;
    }

    public void InteractisPressed(InputAction.CallbackContext value)
    {
        input_Interact = true;
    }

    private void InteractisReleased(InputAction.CallbackContext value)
    {
        input_Interact = false;
    }

    private void Alpha1isPressed(InputAction.CallbackContext value)
    {
        input_A1 = true;
        // inventoryController.changeWeapon(0); //1 because list indexes from 0 - see changeWeapon
    }

    private void Alpha1isReleased(InputAction.CallbackContext value)
    {
        input_A1 = false;
    }

    private void Alpha2isPressed(InputAction.CallbackContext value)
    {
        input_A2 = true;
        // inventoryController.changeWeapon(1); //1 because lst indexes from 0 - see changeWeapon
    }


    private void Alpha2isReleased(InputAction.CallbackContext value)
    {
        input_A2 = false;

    }





    #endregion


    public bool getAcceptingInputs()
    {
        return AcceptingInputs;
    }

    public void setAcceptingInputs(bool setTo)
    {
        AcceptingInputs = setTo;
        if (setTo == false)
        {
            input_A1 = false;
            input_Jump = false;
            input_Sprint = false;
            input_Interact = false;
            input_Fire1 = false;
            input_Fire2 = false;
            input_Fire3 = false;
            input_Reload = false;
            input_A1 = false;
            input_A2 = false;


        }
    }

}
