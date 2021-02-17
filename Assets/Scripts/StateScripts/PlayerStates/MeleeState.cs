using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeState : BaseState
{
    private bool _buttonSmash = false;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        _buttonSmash = false;
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBackFromAnimation;
        controllerReference.DetectionSystem.OnAttackSuccessful += PreformHit;
        controllerReference.AgentAnimations.TriggerMeleeAnimation();
    }

    public virtual void TransitionBackFromAnimation()
    {
        controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBackFromAnimation;
        controllerReference.DetectionSystem.OnAttackSuccessful -= PreformHit;
    }

    public void DetermindNextState(BaseState nextState, BaseState returnState)
    {
        if (_buttonSmash == true)
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
        if(_buttonSmash == false)
        {
            _buttonSmash = true;
        }
    }

    public override void FixedUpdate()
    {
        controllerReference.AgentAimController.SetCameraToMovePlayer();
    }
}
