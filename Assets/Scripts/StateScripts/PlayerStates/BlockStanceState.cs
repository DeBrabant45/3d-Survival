using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStanceState : BaseState
{
    protected WeaponItemSO equippedItem;
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        equippedItem = ((WeaponItemSO)ItemDataManager.Instance.GetItemData(controllerReference.InventorySystem.EquippedWeaponID));
        controllerReference.AgentAnimations.SetBoolForAnimation(equippedItem.BlockStanceAnimation, true);
        controllerReference.PlayerStat.BlockAttack.IsBlocking = true;
        controllerReference.PlayerStat.BlockAttack.OnBlockSuccessful += BlockReaction;
    }

    private void BlockReaction()
    {
        controllerReference.PlayerStat.BlockAttack.OnBlockSuccessful -= BlockReaction;
        controllerReference.AgentAnimations.SetBoolForAnimation(equippedItem.BlockStanceAnimation, false);
        controllerReference.TransitionToState(controllerReference.blockReactionState);
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.Movement.HandleMovement(input);
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.PlayerStat.BlockAttack.IsBlocking = false;
        controllerReference.TransitionToState(controllerReference.meleeWeaponAttackStanceState);
        controllerReference.AgentAnimations.SetBoolForAnimation(equippedItem.BlockStanceAnimation, false);
    }

    public override void Update()
    {
        base.Update();
        controllerReference.DetectionSystem.PreformDetection(controllerReference.InputFromPlayer.MovementDirectionVector);
        HandleMovement(controllerReference.InputFromPlayer.MovementInputVector);
        HandleCameraDirection(controllerReference.InputFromPlayer.MovementDirectionVector);
    }

}
