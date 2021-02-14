using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAimState : AimState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.ActivateMeleeWeaponStanceAnimation();
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAnimations.DeactivateMeleeWeaponStanceAnimation();
        controllerReference.TransitionToState(controllerReference.meleeWeaponAttackOne);
    }

    public override void HandleAimInput()
    {
        base.HandleAimInput();
        controllerReference.AgentAnimations.DeactivateMeleeWeaponStanceAnimation();
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
