using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class FallingState : JumpState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            controllerReference.AgentAnimations.SetTriggerForAnimation("fall");
            controllerReference.Movement.SetCompletedJumpFalse();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}