using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryState : BaseState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
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
        if(controllerReference.PreviousState == controllerReference.meleeWeaponAttackStanceState)
        {
            controllerReference.TransitionToState(controllerReference.meleeWeaponAttackStanceState);
        }
        else if(controllerReference.PreviousState == controllerReference.rangedWeaponAttackStanceState)
        {
            controllerReference.TransitionToState(controllerReference.rangedWeaponAttackStanceState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.idleState);
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
        controllerReference.TransitionToState(controllerReference.placementState);
    }
}
