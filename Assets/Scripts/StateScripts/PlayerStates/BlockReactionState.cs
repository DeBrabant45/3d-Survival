using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockReactionState : BaseState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.SetTriggerForAnimation(WeaponItem.BlockReactionAnimation);
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger += TransitionBack;
    }

    private void TransitionBack()
    {
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= TransitionBack;
        if(controllerReference.PlayerStat.BlockAttack.IsBlocking == false)
        {
            controllerReference.TransitionToState(controllerReference.meleeWeaponAttackStanceState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.blockStanceState);
        }
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.PlayerStat.BlockAttack.IsBlocking = false;
    }
}
