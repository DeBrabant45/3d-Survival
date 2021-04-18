using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeState : BaseState, IAttackable
{
    private bool _isComboTriggered = false;
    private WeaponItemSO _equippedItem;
    public WeaponItemSO EquippedWeapon { get => _equippedItem; }

    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        _isComboTriggered = false;
        controllerReference.Movement.StopMovement();
        SetWeaponInHand();
        controllerReference.ItemSlot.DamageCollider.OnCollisionSuccessful += PreformAttack;
        controllerReference.AgentAnimations.SetTriggerForAnimation(_equippedItem.AttackTriggerAnimation);
        controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBackFromAnimation;
        controllerReference.PlayerStat.ReduceStamina(10);
    }

    public virtual void PreformAttack(Collider hitObject)
    {
        var hittable = hitObject.GetComponent<IHittable>();
        if (hittable != null && hitObject.gameObject != controllerReference.gameObject) 
        {
            hittable.GetHit(_equippedItem);
        }
    }

    private void SetWeaponInHand()
    {
        if (controllerReference.InventorySystem.WeaponEquipped)
        {
            _equippedItem = ((WeaponItemSO)ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID));
        }
        else
        {
            _equippedItem = controllerReference.UnarmedAttack;
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
