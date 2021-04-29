using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAimState : BaseState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.AgentAnimations.SetBoolForAnimation(((RangedWeaponItemSO)WeaponItem).WeaponAimAnimation, true);
        controllerReference.AgentAimController.IsHandsConstraintActive = true;
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.TransitionToState(controllerReference.rangedWeaponAttackState);
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.AgentAimController.IsHandsConstraintActive = false;
        controllerReference.AgentAnimations.SetBoolForAnimation(((RangedWeaponItemSO)WeaponItem).WeaponAimAnimation, false);
        controllerReference.TransitionToState(controllerReference.rangedWeaponAttackStanceState);
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
