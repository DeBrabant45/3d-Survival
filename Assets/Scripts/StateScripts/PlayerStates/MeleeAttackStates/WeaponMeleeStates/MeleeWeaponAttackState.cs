using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class MeleeWeaponAttackState : MeleeState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
        }

        public override void TransitionBackFromAnimation()
        {
            base.TransitionBackFromAnimation();
            DetermindNextState(stateMachine.MeleeWeaponAttackState, stateMachine.MeleeWeaponAttackStanceState);
        }
    }
}