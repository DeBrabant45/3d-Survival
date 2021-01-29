using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadRangedWeaponState : BaseState
{
    private ItemSO _equippedWeapon;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        if(controllerReference.InventorySystem.WeaponEquipped)
        {
            _equippedWeapon = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
            PreformWeaponReload();
            controllerReference.TransitionToState(controllerReference.movementState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.PreviousState);
        }
    }

    private void PreformWeaponReload()
    {
        if(controllerReference.AmmoSystem.IsAmmoAvailable() == true && _equippedWeapon.GetType() == typeof(RangedWeaponItemSO))
        {
            var itemSlotGun = controllerReference.ItemSlot.GetComponentInChildren<GunAmmo>();
            if(itemSlotGun != null && itemSlotGun.IsAmmoEmpty() == true)
            {
                controllerReference.AgentAnimations.TrigggerReloadWeaponAnimation();
                controllerReference.AmmoSystem.ReloadAmmoRequest(((RangedWeaponItemSO)_equippedWeapon).MaxAmmoCount);
                itemSlotGun.ReloadAmmoCount();
            }
        }
    }
}
