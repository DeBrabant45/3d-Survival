using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockReactionState : BaseState
{
    protected WeaponItemSO equippedItem;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        equippedItem = ((WeaponItemSO)ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID));
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.SetTriggerForAnimation(equippedItem.BlockReactionAnimation);
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger += TransitionBack;
    }

    private void TransitionBack()
    {
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= TransitionBack;
        if(controllerReference.PlayerStat.BlockAttack.IsBlocking == false)
        {
            controllerReference.TransitionToState(controllerReference.meleeWeaponAttackStanceState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.blockStanceState);
        }
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.PlayerStat.BlockAttack.IsBlocking = false;
    }
}
