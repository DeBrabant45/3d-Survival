using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAimState : AimState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.IsMeleeWeaponStanceAnimationActive(true);
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAnimations.IsMeleeWeaponStanceAnimationActive(false);
        controllerReference.TransitionToState(controllerReference.meleeWeaponAttackOne);
    }

    public override void HandleAimInput()
    {
        base.HandleAimInput();
        controllerReference.AgentAnimations.IsMeleeWeaponStanceAnimationActive(false);
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
