using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public abstract class MeleeState : BaseState, IAttackable
    {
        private bool _isComboTriggered = false;
        public WeaponItemSO EquippedWeapon { get => WeaponItem; }

        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            _isComboTriggered = false;
            controllerReference.Movement.StopMovement();
            controllerReference.ItemSlot.DamageCollider.OnCollisionSuccessful += PreformAttack;
            controllerReference.AgentAnimations.SetTriggerForAnimation(WeaponItem.AttackTriggerAnimation);
            controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBackFromAnimation;
            controllerReference.AgentStamina.ReduceStamina(10);
        }

        public virtual void PreformAttack(Collider hitObject)
        {
            var hittable = hitObject.GetComponent<IHittable>();
            if (hittable != null && hitObject.gameObject != controllerReference.gameObject)
            {
                var spawnAttackHitEffect = new SpawnGameObject(WeaponItem.AttackHitEffect);
                spawnAttackHitEffect.CreateTemporaryObject(controllerReference.ItemSlotTransform);
                hittable.GetHit(WeaponItem);
            }
        }

        public virtual void TransitionBackFromAnimation()
        {
            controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBackFromAnimation;
            controllerReference.ItemSlot.DamageCollider.OnCollisionSuccessful -= PreformAttack;
        }

        public void DetermindNextState(BaseState nextState, BaseState returnState)
        {
            if (_isComboTriggered == true && controllerReference.AgentStamina.Stamina > 0)
            {
                stateMachine.TransitionToState(nextState);
            }
            else
            {
                stateMachine.TransitionToState(returnState);
            }
        }

        public override void HandlePrimaryInput()
        {
            if (_isComboTriggered == false)
            {
                _isComboTriggered = true;
            }
        }
    }
}