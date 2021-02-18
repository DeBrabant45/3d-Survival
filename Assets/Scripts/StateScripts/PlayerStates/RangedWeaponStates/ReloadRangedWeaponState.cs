using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadRangedWeaponState : BaseState
{
    private ItemSO _equippedWeapon;
    private GunAmmo itemSlotGun;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.OnFinishedReloading += TransitionBackAfterReloadingAnimation;
        itemSlotGun = controllerReference.ItemSlot.GetComponentInChildren<GunAmmo>();
        if (itemSlotGun != null)
        {
            if (controllerReference.AmmoSystem.IsAmmoAvailable() && itemSlotGun.IsAmmoEmpty() == true)
            {
                _equippedWeapon = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
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
        controllerReference.AgentAnimations.TrigggerReloadWeaponAnimation();
        controllerReference.AmmoSystem.ReloadAmmoRequest(((RangedWeaponItemSO)_equippedWeapon).MaxAmmoCount);
        itemSlotGun.ReloadAmmoCount();
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

    public override void FixedUpdate()
    {
        controllerReference.AgentAimController.SetCameraToMovePlayer();
    }
}
