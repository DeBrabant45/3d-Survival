using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : BaseState
{
    private float _defaultFallingDelay = 0.2f;
    private float _fallingDelay = 0;

    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        _fallingDelay = _defaultFallingDelay;
    }

    public override void HandleCameraDirection(Vector3 input)
    {
        base.HandleCameraDirection(input);
        controllerReference.Movement.HandleMovementDirection(input);
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.Movement.HandleMovement(input);
    }

    public override void HandleJumpInput()
    {
        controllerReference.TransitionToState(controllerReference.jumpState);
    }

    public override void HandleInventoryInput()
    {
        controllerReference.TransitionToState(controllerReference.inventoryState);
    }

    public override void HandlePrimaryInput()
    {
        base.HandlePrimaryInput();
        if (controllerReference.InventorySystem.WeaponEquipped)
        {
            controllerReference.TransitionToState(controllerReference.attackState);
        }
        else
        {
            Debug.Log("No weapon set, cannot perform attack");
        }
        //controllerReference.TransitionToState(controllerReference.interactState);
    }

    public override void HandleSecondaryInput()
    {
        base.HandleSecondaryInput();
        controllerReference.TransitionToState(controllerReference.interactState);
    }

    public override void HandleHotBarInput(int hotbarKey)
    {
        base.HandleHotBarInput(hotbarKey);
        controllerReference.InventorySystem.HotbarShortKeyHandler(hotbarKey);
    }

    public override void HandleMenuInput()
    {
        base.HandleMenuInput();
        controllerReference.TransitionToState(controllerReference.menuState);
    }

    public override void Update()
    {
        base.Update();
        controllerReference.DetectionSystem.PreformDetection(controllerReference.InputFromPlayer.MovementDirectionVector);
        HandleMovement(controllerReference.InputFromPlayer.MovementInputVector);
        HandleCameraDirection(controllerReference.InputFromPlayer.MovementDirectionVector);
        if(controllerReference.Movement.CharacterIsGrounded() == false)
        {
            if(_fallingDelay > 0)
            {
                _fallingDelay -= Time.deltaTime;
                return;
            }
            controllerReference.TransitionToState(controllerReference.fallingState);
        }
        else
        {
            _fallingDelay = _defaultFallingDelay;
        }
    }
}
