using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Camera _mainCamera;
    private float _previousPrimaryActionInput = 0;
    private float _previousSecondaryActionInput = 0;

    public Vector2 MovementInputVector { get; private set; }
    public Vector3 MovementDirectionVector { get; private set; }
    public Action OnJump { get; set; }
    public Action OnToggleInventory { get; set; }
    public Action<int> OnHotBarKey { get; set; }
    public Action OnPrimaryAction { get; set; }
    public Action OnSecondaryClickAction { get; set; }
    public Action OnMenuToggledKey { get; set; }
    public Action OnReload { get; set; }
    public Action OnAim { get; set; }
    public Action OnSecondaryHeldDownAction { get; set; }
    public Action OnSecondaryUpAction { get; set; }

    private void Awake()
    {
        _mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CheckMenuButton();
        GetMovementInput();
        GetMovementDirection();
        GetJumpInput();
        GetInventoryInput();
        GetHotBarInput();
        GetPrimaryAction();
        GetSecondaryClickAction();
        GetReloadInput();
        GetAimInput();
        GetSecondaryHeldDownAction();
        GetSecondaryUpAction();
    }

    private void GetSecondaryUpAction()
    {
        if (Input.GetMouseButtonUp(1))
        {
            OnSecondaryUpAction?.Invoke();
        }
    }

    private void GetSecondaryHeldDownAction()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnSecondaryHeldDownAction?.Invoke();
        }
    }

    private void GetReloadInput()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            OnReload?.Invoke();
        }
    }

    private void GetAimInput()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnAim?.Invoke();
        }
    }

    private void CheckMenuButton()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnMenuToggledKey?.Invoke();
        }
    }

    private void GetSecondaryClickAction()
    {
        var inputValue = Input.GetAxisRaw("Fire2");
        if (_previousSecondaryActionInput == 0)
        {
            if (inputValue >= 1)
            {
                OnSecondaryClickAction?.Invoke();
            }
        }
        _previousSecondaryActionInput = inputValue;
    }

    private void GetPrimaryAction()
    {
        var inputValue = Input.GetAxisRaw("Fire1");
        if(_previousPrimaryActionInput == 0)
        {
            if(inputValue >= 1)
            {
                OnPrimaryAction?.Invoke();
            }
        }
        _previousPrimaryActionInput = inputValue;
    }

    private void GetHotBarInput()
    {
        char hotbar0 = '0';
        for (int i = 0; i < 10; i++)
        {
            KeyCode keyCode = (KeyCode)((int)hotbar0 + i);
            if(Input.GetKeyDown(keyCode))
            {
                OnHotBarKey?.Invoke(i);
                return;
            }
        }
    }

    private void GetInventoryInput()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            OnToggleInventory?.Invoke();
        }
    }

    private void GetJumpInput()
    {
        if(Input.GetAxisRaw("Jump") > 0)
        {
            OnJump?.Invoke();
        }
    }

    private void GetMovementDirection()
    {
        var cameraForwardDirection = _mainCamera.transform.forward;
        //Debug.DrawRay(_mainCamera.transform.position, cameraForwardDirection * 10, Color.red);
        MovementDirectionVector = Vector3.Scale(cameraForwardDirection, (Vector3.right + Vector3.forward));
        ////Debug.DrawRay(_mainCamera.transform.position, MovementDirectionVector * 10, Color.green);
    }

    private void GetMovementInput()
    {
        MovementInputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
