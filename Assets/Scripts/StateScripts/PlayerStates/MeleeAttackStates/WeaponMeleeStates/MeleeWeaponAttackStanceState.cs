using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class MeleeWeaponAttackStanceState : AttackStanceState
    {
        public override void HandlePrimaryInput()
        {
            controllerReference.AgentAimController.IsAimActive = false;
            if (controllerReference.AgentStamina.Stamina > 0)
            {
                controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.AttackStance, false);
                stateMachine.TransitionToState(stateMachine.MeleeWeaponAttackState);
            }
        }

        public override void HandleSecondaryHeldDownInput()
        {
            stateMachine.TransitionToState(stateMachine.BlockStanceState);
        }
    }
}