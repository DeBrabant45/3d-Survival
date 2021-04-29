using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemState : BaseState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.AgentAnimations.SetTriggerForAnimation("equipItem");
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger += EquipItem;
    }

    public void EquipItem()
    {
        ItemSpawnManager.Instance.SwapBackItemToPlayersHand();
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= EquipItem;
        if (controllerReference.EquippedItem.WeaponTypeSO == WeaponType.Melee)
        {
            controllerReference.TransitionToState(controllerReference.meleeWeaponAttackStanceState);
        }
        else
        {
            controllerReference.TransitionToState(controllerReference.rangedWeaponAttackStanceState);
        }
    }

}
