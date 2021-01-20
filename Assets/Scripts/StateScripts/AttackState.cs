using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBack;
        controllerReference.AgentAnimations.TriggerAttackAnimation();
        controllerReference.GameManager.AudioManager.WeaponAttackSound.PlayAttackSound(controllerReference.AttackSwingSound);
        controllerReference.DetectionSystem.OnAttackSuccessful += PreformHit;
    }

    private void TransitionBack()
    {
        controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBack;
        controllerReference.DetectionSystem.OnAttackSuccessful -= PreformHit;
        controllerReference.TransitionToState(controllerReference.movementState);
    }

    public void PreformHit(Collider hitObject, Vector3 hitPosition)
    {
        var hittable = hitObject.GetComponent<IHittable>();
        if(hittable != null)
        {
            var equippedItem = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
            hittable.GetHit((WeaponItemSO)equippedItem, hitPosition);
        }
    }
}
