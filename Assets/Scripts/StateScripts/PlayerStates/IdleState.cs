using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class IdleState : MovementState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
        }

        public override void HandleEquipItemInput()
        {
            if (controllerReference.InventorySystem.WeaponEquipped)
            {
                stateMachine.TransitionToState(stateMachine.EquipItemState);
            }
            else
            {
                stateMachine.TransitionToState(stateMachine.MeleeWeaponAttackStanceState);
            }
        }

        public override void HandleInventoryInput()
        {
            stateMachine.TransitionToState(stateMachine.InventoryState);
        }

        public override void HandlePrimaryInput()
        {
            base.HandlePrimaryInput();
        }

        public override void HandleSecondaryClickInput()
        {
            base.HandleSecondaryClickInput();
            stateMachine.TransitionToState(stateMachine.InteractState);
        }

        public override void HandleHotBarInput(int hotbarKey)
        {
            base.HandleHotBarInput(hotbarKey);
            controllerReference.InventorySystem.HotbarShortKeyHandler(hotbarKey);
        }

        public override void HandleMenuInput()
        {
            base.HandleMenuInput();
            stateMachine.TransitionToState(stateMachine.MenuState);
        }

        public override void HandlePlacementInput()
        {
            base.HandlePlacementInput();
            stateMachine.TransitionToState(stateMachine.PlacementState);
        }
    }
}