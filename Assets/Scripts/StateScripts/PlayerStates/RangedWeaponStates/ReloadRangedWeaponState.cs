using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadRangedWeaponState : BaseState
{
    private IAmmo _rangedItemAmmo;
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.AgentAnimations.OnFinishedReloading += TransitionBackAfterReloadingAnimation;
        _rangedItemAmmo = controllerReference.ItemSlotTransform.GetComponentInChildren<IAmmo>();
        if (_rangedItemAmmo != null)
        {
            if (controllerReference.AmmoSystem.IsAmmoAvailable(((RangedWeaponItemSO)WeaponItem).AmmoType) && _rangedItemAmmo.IsAmmoEmpty() == true)
            {
                PreformWeaponReload();
            }
            else if (controllerReference.AgentAimController.IsHandsConstraintActive == true)
            {
                controllerReference.TransitionToState(controllerReference.rangedWeaponAimState);
            }
            else 
            {
                controllerReference.TransitionToState(controllerReference.rangedWeaponAttackStanceState);
            }
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.PreviousState);
        }
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.AgentAimController.IsHandsConstraintActive = false;
        controllerReference.AgentAnimations.SetBoolForAnimation(((RangedWeaponItemSO)WeaponItem).WeaponAimAnimation, false);
    }

    private void PreformWeaponReload()
    {
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.SetTriggerForAnimation(((RangedWeaponItemSO)WeaponItem).ReloadAnimationTrigger);
        controllerReference.AmmoSystem.ReloadAmmoRequest(((RangedWeaponItemSO)WeaponItem).AmmoType, (((RangedWeaponItemSO)WeaponItem)).MaxAmmoCount);
        _rangedItemAmmo.ReloadAmmoCount();
    }

    private void TransitionBackAfterReloadingAnimation()
    {
        controllerReference.AgentAnimations.OnFinishedReloading -= TransitionBackAfterReloadingAnimation;
        if(controllerReference.AgentAimController.IsHandsConstraintActive == true)
        {
            controllerReference.TransitionToState(controllerReference.rangedWeaponAimState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.rangedWeaponAttackStanceState);
        }
    }
}
