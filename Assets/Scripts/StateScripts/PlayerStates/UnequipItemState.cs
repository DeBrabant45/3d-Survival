using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class UnequipItemState : MovementState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            controllerReference.AgentAnimations.SetTriggerForAnimation("unequipItem");
            controllerReference.AgentAnimations.OnAnimationFunctionTrigger += UnequipItem;
        }

        public void UnequipItem()
        {
            ItemSpawnManager.Instance.SwapHandItemToPlayersBack(controllerReference.InventorySystem.WeaponEquipped);
            controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= UnequipItem;
            stateMachine.TransitionToState(stateMachine.IdleState);
        }
    }
}
