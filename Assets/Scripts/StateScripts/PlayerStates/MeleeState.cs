using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeState : BaseState
{
    protected int count = 0;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        count = 0;
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBackFromAnimation;
        controllerReference.DetectionSystem.OnAttackSuccessful += PreformHit;
    }

    public virtual void TransitionBackFromAnimation()
    {
        controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBackFromAnimation;
        controllerReference.DetectionSystem.OnAttackSuccessful -= PreformHit;
    }

    public void DetermindNextState(BaseState nextState, BaseState returnState)
    {
        if (count >= 1)
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
        count = 1;
    }
}
