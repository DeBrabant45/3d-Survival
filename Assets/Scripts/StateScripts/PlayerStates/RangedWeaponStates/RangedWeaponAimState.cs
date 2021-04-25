using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAimState : BaseState
{
    private RangedWeaponItemSO _equippedWeapon;

    public RangedWeaponAimState(RangedWeaponItemSO rangedWeapon)
    {
        _equippedWeapon = rangedWeapon;
    }
    
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.SetBoolForAnimation(_equippedWeapon.WeaponAimAnimation, true);
        controllerReference.AgentAimController.IsHandsConstraintActive = true;
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.TransitionToState(controllerReference.rangedWeaponAttackState);
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.AgentAimController.IsHandsConstraintActive = false;
        controllerReference.AgentAnimations.SetBoolForAnimation(_equippedWeapon.WeaponAimAnimation, false);
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
