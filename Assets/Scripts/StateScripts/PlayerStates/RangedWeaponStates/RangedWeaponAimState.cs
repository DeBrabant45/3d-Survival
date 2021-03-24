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
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAnimations.SetBoolForAnimation(_equippedWeapon.WeaponAimAnimation, false);
        controllerReference.TransitionToState(controllerReference.rangedWeaponAttackState);
    }

    public override void HandleEquipItemInput()
    {
        base.HandleEquipItemInput();
        controllerReference.AgentAnimations.SetBoolForAnimation(_equippedWeapon.WeaponAimAnimation, false);
    }

    public override void HandleReloadInput()
    {
        base.HandleReloadInput();
        controllerReference.AgentAimController.IsAimActive = false;
        controllerReference.AgentAnimations.SetBoolForAnimation(_equippedWeapon.WeaponAimAnimation, false);
        controllerReference.TransitionToState(controllerReference.reloadRangedWeaponState);
    }
}
