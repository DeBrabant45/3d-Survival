using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : BaseState
{
    private IAmmo _rangedItemAmmo;
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        _rangedItemAmmo = controllerReference.ItemSlotTransform.GetComponentInChildren<IAmmo>();
        if(_rangedItemAmmo != null)
        {
            if(_rangedItemAmmo.IsAmmoEmpty() == false)
            {
                controllerReference.Movement.StopMovement();
                controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBack;
                controllerReference.AgentAnimations.SetTriggerForAnimation(WeaponItem.AttackTriggerAnimation);
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
        controllerReference.AgentAnimations.SetBoolForAnimation(((RangedWeaponItemSO)WeaponItem).WeaponAimAnimation, false);
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
        AddDamageToTarget(target);
        AddWeaponImpactForce(hit);
        CreateWeaponImpactEffect(hit);
    }

    private void AddDamageToTarget(IHittable target)
    {
        if (target != null)
        {
            target.GetHit(WeaponItem);
        }
    }

    private void AddWeaponImpactForce(RaycastHit hit)
    {
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * WeaponItem.WeaponImpactForce);
        }
    }

    private void CreateWeaponImpactEffect(RaycastHit hit)
    {
        var spawnAttackHitEffect = new SpawnGameObject(WeaponItem.AttackHitEffect);
        spawnAttackHitEffect.CreateTemporaryObject(hit.point, Quaternion.LookRotation(hit.normal), 2f);
        //GameObject impactEffect = GameObject.Instantiate(WeaponItem.AttackHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //GameObject.Destroy(impactEffect, 2f);
    }
}
