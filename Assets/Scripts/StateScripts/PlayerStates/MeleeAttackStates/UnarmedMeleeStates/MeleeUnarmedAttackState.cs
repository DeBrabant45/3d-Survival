using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class MeleeUnarmedAttackState : MeleeState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
        }

        public override void TransitionBackFromAnimation()
        {
            base.TransitionBackFromAnimation();
            DetermindNextState(stateMachine.MeleeUnarmedAttackState, stateMachine.MeleeWeaponAttackStanceState);
        }

        public override void PreformAttack(Collider hitObject)
        {
            var hittable = hitObject.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.GetHit(controllerReference.UnarmedAttack);
            }
        }
    }
}