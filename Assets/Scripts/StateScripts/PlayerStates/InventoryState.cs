using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class InventoryState : BaseState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            controllerReference.InventorySystem.ToggleInventory();
            controllerReference.GameManager.AudioManager.PauseAllMapSounds();
            controllerReference.CraftingSystem.ToggleCraftingUI();
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public override void HandleInventoryInput()
        {
            base.HandleInventoryInput();
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controllerReference.InventorySystem.ToggleInventory();
            controllerReference.CraftingSystem.ToggleCraftingUI();
            controllerReference.GameManager.AudioManager.StartAllMapSounds();
            if (stateMachine.PreviousState == stateMachine.MeleeWeaponAttackStanceState)
            {
                stateMachine.TransitionToState(stateMachine.MeleeWeaponAttackStanceState);
            }
            else if (stateMachine.PreviousState == stateMachine.RangedWeaponAttackStanceState)
            {
                stateMachine.TransitionToState(stateMachine.RangedWeaponAttackStanceState);
            }
            else
            {
                stateMachine.TransitionToState(stateMachine.IdleState);
            }
        }

        private void DisableInventoryUI()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controllerReference.InventorySystem.ToggleInventory();
            controllerReference.CraftingSystem.ToggleCraftingUI(true);
            controllerReference.GameManager.AudioManager.StartAllMapSounds();
        }

        public override void HandlePlacementInput()
        {
            base.HandlePlacementInput();
            DisableInventoryUI();
            stateMachine.TransitionToState(stateMachine.PlacementState);
        }
    }
}