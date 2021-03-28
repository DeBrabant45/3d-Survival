using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeState : BaseState
{
    private bool _isComboTriggered = false;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        _isComboTriggered = false;
        controllerReference.Movement.StopMovement();
        TriggerCorrectAttackAnimation();
        controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBackFromAnimation;
        controllerReference.DetectionSystem.OnAttackSuccessful += PreformHit;
        controllerReference.PlayerStat.ReduceStamina(10);
    }

    private void TriggerCorrectAttackAnimation()
    {
        if (controllerReference.InventorySystem.WeaponEquipped)
        {
            var equippedItem = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
            controllerReference.AgentAnimations.SetTriggerForAnimation(((WeaponItemSO)equippedItem).AttackTriggerAnimation);
        }
        else
        {
            controllerReference.AgentAnimations.SetTriggerForAnimation("meleeUnarmedAttack");
        }
    }

    public virtual void TransitionBackFromAnimation()
    {
        controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBackFromAnimation;
        controllerReference.DetectionSystem.OnAttackSuccessful -= PreformHit;
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

    public virtual void PreformHit(Collider hitObject, Vector3 hitPosition)
    {
        var hittable = hitObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            var equippedItem = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
            hittable.GetHit((WeaponItemSO)equippedItem, hitPosition);
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
