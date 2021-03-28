using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAimState : AimState
{
    private RangedWeaponItemSO _equippedWeapon;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        _equippedWeapon = (RangedWeaponItemSO)ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID);
        controllerReference.AgentAnimations.SetBoolForAnimation(_equippedWeapon.WeaponAimAnimation, true);
        controllerReference.AgentAimController.IsHandsConstraintActive = true;
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.TransitionToState(controllerReference.rangedWeaponAttackState);
    }

    public override void HandleEquipItemInput()
    {
        controllerReference.AgentAimController.IsHandsConstraintActive = false;
        base.HandleEquipItemInput();
        controllerReference.AgentAnimations.SetBoolForAnimation(_equippedWeapon.WeaponAimAnimation, false);
    }

    public override void HandleReloadInput()
    {
        base.HandleReloadInput();
        controllerReference.AgentAimController.IsAimActive = false;
        controllerReference.TransitionToState(controllerReference.reloadRangedWeaponState);
    }
}
