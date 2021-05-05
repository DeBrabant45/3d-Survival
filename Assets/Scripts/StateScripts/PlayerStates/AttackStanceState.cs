using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStanceState : BaseState
{
    private WeaponItemSO _pastItem;
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.Movement.StopMovement();
        SetAimValuesToActive();
        if (ItemSpawnManager.Instance.IsWeaponOnBackAndInHand || (_pastItem != weapon && _pastItem != null))
        {
            HandleEquipItemInput();
        }
        else
        {
            if(_pastItem != weapon)
            {
                _pastItem = weapon;
            }
            controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.AttackStance, true);
        }
    }

    private void SetAimValuesToActive()
    {
        controllerReference.AgentAimController.SetZoomInFieldOfView();
        controllerReference.AgentAimController.IsAimActive = true;
        controllerReference.AgentAimController.AimCrossHair.enabled = true;
    }

    public override void HandleEquipItemInput() 
    {
        controllerReference.AgentAnimations.SetBoolForAnimation(_pastItem.AttackStance, false);
        SetAimValuesToInactive();
        if(_pastItem == controllerReference.UnarmedAttack)
        {
            controllerReference.TransitionToState(controllerReference.movementState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.unequipItemState);
        }
        _pastItem = null;
    }

    public void SetAimValuesToInactive()
    {
        controllerReference.AgentAimController.SetZoomOutFieldOfView();
        controllerReference.AgentAimController.AimCrossHair.enabled = false;
        controllerReference.AgentAimController.IsAimActive = false;
    }

    public override void HandleInventoryInput()
    {
        controllerReference.TransitionToState(controllerReference.inventoryState);
    }

    public override void HandleMenuInput()
    {
        base.HandleMenuInput();
        controllerReference.TransitionToState(controllerReference.menuState);
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.Movement.HandleMovement(input);
    }

    public override void Update()
    {
        base.Update();
        controllerReference.DetectionSystem.PreformDetection(controllerReference.InputFromPlayer.MovementDirectionVector);
        HandleMovement(controllerReference.InputFromPlayer.MovementInputVector);
        HandleCameraDirection(controllerReference.InputFromPlayer.MovementDirectionVector);
    }
}
