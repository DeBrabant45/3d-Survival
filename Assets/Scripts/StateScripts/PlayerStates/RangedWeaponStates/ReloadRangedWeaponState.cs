using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class ReloadRangedWeaponState : BaseState
    {
        private IAmmo _rangedItemAmmo;
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
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
                    stateMachine.TransitionToState(stateMachine.RangedWeaponAimState);
                }
                else
                {
                    stateMachine.TransitionToState(stateMachine.RangedWeaponAttackStanceState);
                }
            }
            else
            {
                stateMachine.TransitionToState(stateMachine.PreviousState);
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
            if (controllerReference.AgentAimController.IsHandsConstraintActive == true)
            {
                stateMachine.TransitionToState(stateMachine.RangedWeaponAimState);
            }
            else
            {
                stateMachine.TransitionToState(stateMachine.RangedWeaponAttackStanceState);
            }
        }
    }
}