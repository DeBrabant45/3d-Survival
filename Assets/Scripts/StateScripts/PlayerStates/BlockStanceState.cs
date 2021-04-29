using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStanceState : BaseState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.BlockStanceAnimation, true);
        controllerReference.PlayerStat.BlockAttack.IsBlocking = true;
        controllerReference.PlayerStat.BlockAttack.OnBlockSuccessful += BlockReaction;
    }

    private void BlockReaction()
    {
        controllerReference.PlayerStat.BlockAttack.OnBlockSuccessful -= BlockReaction;
        controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.BlockStanceAnimation, false);
        controllerReference.TransitionToState(controllerReference.blockReactionState);
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.Movement.HandleMovement(input);
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.PlayerStat.BlockAttack.IsBlocking = false;
        controllerReference.TransitionToState(controllerReference.meleeWeaponAttackStanceState);
        controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.BlockStanceAnimation, false);
    }

    public override void Update()
    {
        base.Update();
        controllerReference.DetectionSystem.PreformDetection(controllerReference.InputFromPlayer.MovementDirectionVector);
        HandleMovement(controllerReference.InputFromPlayer.MovementInputVector);
        HandleCameraDirection(controllerReference.InputFromPlayer.MovementDirectionVector);
    }

}
