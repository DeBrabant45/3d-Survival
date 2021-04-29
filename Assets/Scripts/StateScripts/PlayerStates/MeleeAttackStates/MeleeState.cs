using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeState : BaseState, IAttackable
{
    private bool _isComboTriggered = false;
    public WeaponItemSO EquippedWeapon { get => WeaponItem; }

    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        _isComboTriggered = false;
        controllerReference.Movement.StopMovement();
        controllerReference.ItemSlot.DamageCollider.OnCollisionSuccessful += PreformAttack;
        controllerReference.AgentAnimations.SetTriggerForAnimation(WeaponItem.AttackTriggerAnimation);
        controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBackFromAnimation;
        controllerReference.PlayerStat.ReduceStamina(10);
    }

    public virtual void PreformAttack(Collider hitObject)
    {
        var hittable = hitObject.GetComponent<IHittable>();
        if (hittable != null && hitObject.gameObject != controllerReference.gameObject) 
        {
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
        if (_isComboTriggered == true && controllerReference.PlayerStat.Stamina > 0)
        {
            controllerReference.TransitionToState(nextState);
        }
        else
        {
            controllerReference.TransitionToState(returnState);
        }
    }

    public override void HandlePrimaryInput()
    {
        if(_isComboTriggered == false)
        {
            _isComboTriggered = true;
        }
    }
}
