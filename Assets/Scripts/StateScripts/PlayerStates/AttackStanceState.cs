using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public abstract class AttackStanceState : MovementState
    {
        private WeaponItemSO _pastItem;
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            controllerReference.Movement.StopMovement();
            SetAimValuesToActive();
            if (ItemSpawnManager.Instance.IsWeaponOnBackAndInHand || (_pastItem != weapon && _pastItem != null))
            {
                HandleEquipItemInput();
            }
            else
            {
                if (_pastItem != weapon)
                {
                    _pastItem = weapon;
                }
                controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.AttackStance, true);
            }
        }

        private void SetAimValuesToActive()
        {
            controllerReference.AgentAimController.SetZoomInFieldOfView();
            controllerReference.AgentAimController.IsAimActive = true;
            controllerReference.AgentAimController.AimCrossHair.enabled = true;
        }

        public override void HandleEquipItemInput()
        {
            controllerReference.AgentAnimations.SetBoolForAnimation(_pastItem.AttackStance, false);
            SetAimValuesToInactive();
            if (_pastItem == controllerReference.UnarmedAttack)
            {
                stateMachine.TransitionToState(stateMachine.IdleState);
            }
            else
            {
                stateMachine.TransitionToState(stateMachine.UnequipItemState);
            }
            _pastItem = null;
        }

        public void SetAimValuesToInactive()
        {
            controllerReference.AgentAimController.SetZoomOutFieldOfView();
            controllerReference.AgentAimController.AimCrossHair.enabled = false;
            controllerReference.AgentAimController.IsAimActive = false;
        }

        public override void HandleInventoryInput()
        {
            stateMachine.TransitionToState(stateMachine.InventoryState);
        }

        public override void HandleMenuInput()
        {
            base.HandleMenuInput();
            stateMachine.TransitionToState(stateMachine.MenuState);
        }
    }
}