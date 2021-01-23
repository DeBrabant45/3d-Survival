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
        controllerReference.DetectionSystem.OnRangeAttackSuccessful += PreformShoot;
    }

    private void TransitionBack()
    {
        controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBack;
        controllerReference.DetectionSystem.OnRangeAttackSuccessful -= PreformShoot;
        controllerReference.TransitionToState(controllerReference.movementState);
    }

    public void PreformShoot(Collider hitObject, Vector3 hitPosition, RaycastHit hit)
    {
        var target = hitObject.transform.GetComponent<Target>();
        var equippedItem = ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
        AddDamageToTarget(target, equippedItem);
        AddWeaponImpactForce(hit, equippedItem);
        CreateWeaponImpactEffect(hit);
    }

    private static void AddDamageToTarget(Target target, ItemSO equippedItem)
    {
        if (target != null)
        {
            Debug.Log(target.transform.name);
            target.TakeDamage(((WeaponItemSO)equippedItem).MaximumDamage);
        }
    }

    private void AddWeaponImpactForce(RaycastHit hit, ItemSO equippedItem)
    {
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * ((WeaponItemSO)equippedItem).WeaponImpactForce);
        }
    }

    private void CreateWeaponImpactEffect(RaycastHit hit)
    {
        GameObject impactEffect = GameObject.Instantiate(controllerReference.DetectionSystem.ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        GameObject.Destroy(impactEffect, 2f);
    }

}
