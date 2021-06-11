using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public abstract class BaseState
    {
        protected PlayerStateMachine stateMachine;
        protected AgentController controllerReference;
        protected WeaponItemSO WeaponItem;

        public virtual void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            this.stateMachine = state;
            this.controllerReference = controller;
            this.WeaponItem = weapon;
        }

        public virtual void HandleMovement(Vector2 input) { }
        public virtual void HandleCameraDirection(Vector3 input) { }
        public virtual void HandleJumpInput() { }
        public virtual void HandleInventoryInput() { }
        public virtual void HandleHotBarInput(int hotbarKey) { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void HandleSecondaryClickInput() { }
        public virtual void HandlePrimaryInput() { }
        public virtual void HandleMenuInput() { }
        public virtual void HandleReloadInput() { }
        public virtual void HandleEquipItemInput() { }
        public virtual void HandlePlacementInput() { }
        public virtual void HandleSecondaryHeldDownInput() { }
        public virtual void HandleSecondaryUpInput() { }
    }
}
