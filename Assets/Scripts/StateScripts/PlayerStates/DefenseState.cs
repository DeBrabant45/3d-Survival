using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.SetBoolForAnimation("meleeWeaponDefense", true);
        controllerReference.PlayerStat.BlockAttack.IsBlocking = true;
        controllerReference.PlayerStat.BlockAttack.OnBlockSuccessful += BlockReaction;
    }

    private void BlockReaction()
    {
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.SetTriggerForAnimation("meleeWeaponBlockReact");
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.Movement.HandleMovement(input);
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.PlayerStat.BlockAttack.IsBlocking = false;
        controllerReference.TransitionToState(controllerReference.meleeWeaponAimState);
        controllerReference.AgentAnimations.SetBoolForAnimation("meleeWeaponDefense", false);
    }

    public override void Update()
    {
        base.Update();
        controllerReference.DetectionSystem.PreformDetection(controllerReference.InputFromPlayer.MovementDirectionVector);
        HandleMovement(controllerReference.InputFromPlayer.MovementInputVector);
        HandleCameraDirection(controllerReference.InputFromPlayer.MovementDirectionVector);
    }

}
