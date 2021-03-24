using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadRangedWeaponState : BaseState
{
    private ItemSO _equippedWeapon;
    private IAmmo _rangedItemAmmo;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.OnFinishedReloading += TransitionBackAfterReloadingAnimation;
        _rangedItemAmmo = controllerReference.ItemSlot.GetComponentInChildren<IAmmo>();
        if (_rangedItemAmmo != null)
        {
            _equippedWeapon = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
            if (controllerReference.AmmoSystem.IsAmmoAvailable(((RangedWeaponItemSO)_equippedWeapon).AmmoType) && _rangedItemAmmo.IsAmmoEmpty() == true)
            {
                PreformWeaponReload();
            }
            else if (controllerReference.AgentAimController.AimCrossHair.IsActive())
            {
                controllerReference.TransitionToState(controllerReference.rangedWeaponAimState);
            }
            else
            {
                controllerReference.TransitionToState(controllerReference.PreviousState);
            }
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.PreviousState);
        }
    }

    private void PreformWeaponReload()
    {
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.SetTriggerForAnimation("reloadWeapon");
        controllerReference.AmmoSystem.ReloadAmmoRequest(((RangedWeaponItemSO)_equippedWeapon).AmmoType, ((RangedWeaponItemSO)_equippedWeapon).MaxAmmoCount);
        _rangedItemAmmo.ReloadAmmoCount();
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
