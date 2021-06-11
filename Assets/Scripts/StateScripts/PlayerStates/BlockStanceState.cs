using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class BlockStanceState : MovementState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.BlockStanceAnimation, true);
            controllerReference.BlockAttack.IsBlocking = true;
            controllerReference.BlockAttack.OnBlockSuccessful += BlockReaction;
        }

        private void BlockReaction()
        {
            controllerReference.BlockAttack.OnBlockSuccessful -= BlockReaction;
            controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.BlockStanceAnimation, false);
            stateMachine.TransitionToState(stateMachine.BlockReactionState);
        }

        public override void HandleSecondaryUpInput()
        {
            controllerReference.BlockAttack.IsBlocking = false;
            stateMachine.TransitionToState(stateMachine.MeleeWeaponAttackStanceState);
            controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.BlockStanceAnimation, false);
        }
    }
}
