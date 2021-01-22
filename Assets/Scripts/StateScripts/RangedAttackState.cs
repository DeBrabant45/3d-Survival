using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBack;
        controllerReference.AgentAnimations.TriggerShootAnimation();
        controllerReference.DetectionSystem.OnAttackSuccessful += PreformShoot;
    }

    private void TransitionBack()
    {
        controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBack;
        controllerReference.DetectionSystem.OnAttackSuccessful -= PreformShoot;
        controllerReference.TransitionToState(controllerReference.movementState);
    }

    public void PreformShoot(Collider hitObject, Vector3 hitPosition)
    {
        var target = hitObject.transform.GetComponent<Target>();
        if (target != null)
        {
            Debug.Log(target.transform.name);
            var equippedItem = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
            target.TakeDamage(((WeaponItemSO)equippedItem).MaximumDamage);
        }
    }
}
