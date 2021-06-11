using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class RangedWeaponAimState : MovementState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            controllerReference.AgentAnimations.SetBoolForAnimation(((RangedWeaponItemSO)WeaponItem).WeaponAimAnimation, true);
            controllerReference.AgentAimController.IsHandsConstraintActive = true;
        }

        public override void HandlePrimaryInput()
        {
            stateMachine.TransitionToState(stateMachine.RangedWeaponAttackState);
        }

        public override void HandleSecondaryUpInput()
        {
            controllerReference.AgentAimController.IsHandsConstraintActive = false;
            controllerReference.AgentAnimations.SetBoolForAnimation(((RangedWeaponItemSO)WeaponItem).WeaponAimAnimation, false);
            stateMachine.TransitionToState(stateMachine.RangedWeaponAttackStanceState);
        }
    }
}