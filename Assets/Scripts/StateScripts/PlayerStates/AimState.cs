using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAimController.SetZoomInFieldOfView();
        controllerReference.AgentAimController.IsAimActive = true;
        controllerReference.AgentAimController.AimCrossHair.enabled = true;
    }


    public override void HandleEquipItemInput()
    {
        SetAimValuesToInactive();
        controllerReference.TransitionToState(controllerReference.unequipItemState);
    }

    public void SetAimValuesToInactive()
    {
        controllerReference.AgentAimController.SetZoomOutFieldOfView();
        controllerReference.AgentAimController.AimCrossHair.enabled = false;
        controllerReference.AgentAimController.IsAimActive = false;
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAimController.IsAimActive = false;
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
