using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementState
{

    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
    }

    public override void HandleEquipItemInput()
    {
        if (controllerReference.InventorySystem.WeaponEquipped)
        {
            controllerReference.TransitionToState(controllerReference.equipItemState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.meleeWeaponAttackStanceState);
        }
    }

    public override void HandleInventoryInput()
    {
        controllerReference.TransitionToState(controllerReference.inventoryState);
    }

    public override void HandlePrimaryInput()
    {
        base.HandlePrimaryInput();
    }

    public override void HandleSecondaryClickInput()
    {
        base.HandleSecondaryClickInput();
        controllerReference.TransitionToState(controllerReference.interactState);
    }

    public override void HandleHotBarInput(int hotbarKey)
    {
        base.HandleHotBarInput(hotbarKey);
        controllerReference.InventorySystem.HotbarShortKeyHandler(hotbarKey);
    }

    public override void HandleMenuInput()
    {
        base.HandleMenuInput();
        controllerReference.TransitionToState(controllerReference.menuState);
    }

    public override void HandlePlacementInput()
    {
        base.HandlePlacementInput();
        controllerReference.TransitionToState(controllerReference.placementState);
    }
}
