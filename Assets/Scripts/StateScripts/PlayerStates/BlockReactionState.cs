using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class BlockReactionState : BaseState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            controllerReference.Movement.StopMovement();
            controllerReference.AgentAnimations.SetTriggerForAnimation(WeaponItem.BlockReactionAnimation);
            controllerReference.AgentAnimations.OnAnimationFunctionTrigger += TransitionBack;
        }

        private void TransitionBack()
        {
            controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= TransitionBack;
            if (controllerReference.BlockAttack.IsBlocking == false)
            {
                stateMachine.TransitionToState(stateMachine.MeleeWeaponAttackStanceState);
            }
            else
            {
                stateMachine.TransitionToState(stateMachine.BlockStanceState);
            }
        }

        public override void HandleSecondaryUpInput()
        {
            controllerReference.BlockAttack.IsBlocking = false;
        }
    }
}