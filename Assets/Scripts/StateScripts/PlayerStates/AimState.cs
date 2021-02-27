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
        controllerReference.AgentAimController.AimCrossHair.enabled = true;
    }

    public override void HandleEquipItemInput()
    {
        controllerReference.TransitionToState(controllerReference.movementState);
        controllerReference.AgentAimController.SetZoomOutFieldOfView();
        controllerReference.AgentAimController.AimCrossHair.enabled = false;
        controllerReference.TransitionToState(controllerReference.unequipItemState);
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
