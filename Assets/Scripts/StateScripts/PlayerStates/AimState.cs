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

    public override void HandleAimInput()
    {
        controllerReference.TransitionToState(controllerReference.movementState);
        controllerReference.AgentAimController.SetZoomOutFieldOfView();
        controllerReference.AgentAimController.AimCrossHair.enabled = false;
    }

    public override void FixedUpdate()
    {
        controllerReference.AgentAimController.SetCameraToMovePlayer();
    }
}
