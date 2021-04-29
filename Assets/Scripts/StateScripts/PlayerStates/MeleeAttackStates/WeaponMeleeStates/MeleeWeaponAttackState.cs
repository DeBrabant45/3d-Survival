using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttackState : MeleeState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
    }

    public override void TransitionBackFromAnimation()
    {
        base.TransitionBackFromAnimation();
        DetermindNextState(controllerReference.meleeWeaponAttackState, controllerReference.meleeWeaponAttackStanceState);
    }
}
