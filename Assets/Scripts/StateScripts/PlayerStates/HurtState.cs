using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class HurtState : BaseState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            controllerReference.Movement.StopMovement();
            controllerReference.AgentAnimations.SetTriggerForAnimation("hurtPlayer");
            controllerReference.AgentAnimations.OnAnimationFunctionTrigger += ReturnBackToState;
        }

        public void ReturnBackToState()
        {
            controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= ReturnBackToState;
            stateMachine.TransitionToState(stateMachine.PreviousState);
        }
    }
}