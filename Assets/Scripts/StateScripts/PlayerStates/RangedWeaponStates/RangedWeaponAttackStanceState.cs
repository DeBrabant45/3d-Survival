using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class RangedWeaponAttackStanceState : AttackStanceState
    {
        public override void HandleSecondaryHeldDownInput()
        {
            stateMachine.TransitionToState(stateMachine.RangedWeaponAimState);
        }

        public override void HandleReloadInput()
        {
            stateMachine.TransitionToState(stateMachine.ReloadRangedWeaponState);
        }
    }
}