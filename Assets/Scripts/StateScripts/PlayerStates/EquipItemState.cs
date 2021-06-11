using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class EquipItemState : MovementState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            controllerReference.AgentAnimations.SetTriggerForAnimation("equipItem");
            controllerReference.AgentAnimations.OnAnimationFunctionTrigger += EquipItem;
        }

        public void EquipItem()
        {
            ItemSpawnManager.Instance.SwapBackItemToPlayersHand();
            controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= EquipItem;
            if (controllerReference.EquippedItem.WeaponTypeSO == WeaponType.Melee)
            {
                controllerReference.Movement.StopMovement();
                stateMachine.TransitionToState(stateMachine.MeleeWeaponAttackStanceState);
            }
            else
            {
                stateMachine.TransitionToState(stateMachine.RangedWeaponAttackStanceState);
            }
        }
    }
}