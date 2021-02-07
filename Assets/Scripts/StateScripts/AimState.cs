using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.Movement.StopMovement();
        controllerReference.InputFromPlayer.PlayerFollowCamera.Priority = 0;
        controllerReference.InputFromPlayer.PlayerAimCamera.Priority = 1;
        controllerReference.AgentAimController.AimCrossHair.enabled = true;
    }

    public override void HandleAimInput()
    {
        controllerReference.TransitionToState(controllerReference.movementState);
        controllerReference.InputFromPlayer.PlayerFollowCamera.Priority = 1;
        controllerReference.InputFromPlayer.PlayerAimCamera.Priority = 0;
        controllerReference.AgentAimController.AimCrossHair.enabled = false;
    }

    public override void Update()
    {
        controllerReference.AgentAimController.SetCameraToMovePlayer();
    }
}
