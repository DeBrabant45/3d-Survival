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
        controllerReference.AgentAnimations.OnFinishedReloading += TransitionBackAfterReloadingAnimation;
        if (controllerReference.InventorySystem.WeaponEquipped)
        {
            _equippedWeapon = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
            PreformWeaponReload();
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

    private void TransitionBackAfterReloadingAnimation()
    {
        controllerReference.AgentAnimations.OnFinishedReloading -= TransitionBackAfterReloadingAnimation;
        if(controllerReference.AgentAimController.AimCrossHair.IsActive())
        {
            controllerReference.TransitionToState(controllerReference.rangedWeaponAimState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.PreviousState);
        }
    }
}
