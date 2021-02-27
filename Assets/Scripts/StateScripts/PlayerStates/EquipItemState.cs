using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.TrigggerEquipWeaponAnimation();
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger += EquipItem;
    }

    public void EquipItem()
    {
        ItemSpawnManager.Instance.SwapBackItemToPlayersHand();
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= EquipItem;
        var equippedItem = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
        if (((WeaponItemSO)equippedItem).WeaponTypeSO == WeaponType.Melee)
        {
            controllerReference.TransitionToState(controllerReference.meleeWeaponAimState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.rangedWeaponAimState);
        }
    }

}
