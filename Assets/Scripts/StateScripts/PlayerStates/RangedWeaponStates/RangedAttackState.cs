using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : BaseState
{
    private RangedWeaponItemSO equippedWeapon;
    private IAmmo _rangedItemAmmo;

    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        _rangedItemAmmo = controllerReference.ItemSlotTransform.GetComponentInChildren<IAmmo>();
        if(_rangedItemAmmo != null)
        {
            if(_rangedItemAmmo.IsAmmoEmpty() == false)
            {
                controllerReference.Movement.StopMovement();
                equippedWeapon = (RangedWeaponItemSO)ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
                controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBack;
                controllerReference.AgentAnimations.SetTriggerForAnimation(equippedWeapon.AttackTriggerAnimation);
                controllerReference.DetectionSystem.OnRangeAttackSuccessful += PreformShoot;
                RemoveAmmoWhenShooting();
            }
            else
            {
                controllerReference.TransitionToState(controllerReference.reloadRangedWeaponState);
            }
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.PreviousState);
        }
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.AgentAimController.IsHandsConstraintActive = false;
        controllerReference.AgentAnimations.SetBoolForAnimation(equippedWeapon.WeaponAimAnimation, false);
    }

    private void RemoveAmmoWhenShooting()
    {
        _rangedItemAmmo.RemoveFromCurrentAmmoCount();
    }

    private void TransitionBack()
    {
        controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBack;
        controllerReference.DetectionSystem.OnRangeAttackSuccessful -= PreformShoot;
        if (controllerReference.AgentAimController.IsHandsConstraintActive == true)
        {
            controllerReference.TransitionToState(controllerReference.rangedWeaponAimState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.rangedWeaponAttackStanceState);
        }
    }

    public void PreformShoot(Collider hitObject, Vector3 hitPosition, RaycastHit hit)
    {
        var target = hitObject.transform.GetComponent<IHittable>();
        AddDamageToTarget(target, equippedWeapon);
        AddWeaponImpactForce(hit, equippedWeapon);
        CreateWeaponImpactEffect(hit);
    }

    private static void AddDamageToTarget(IHittable target, ItemSO equippedItem)
    {
        if (target != null)
        {
            target.GetHit(((WeaponItemSO)equippedItem));
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
