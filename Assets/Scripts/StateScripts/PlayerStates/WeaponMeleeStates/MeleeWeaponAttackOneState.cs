using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttackOneState : MeleeState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.TriggerMeleeAttackOneAnimation();
    }

    public override void TransitionBackFromAnimation()
    {
        base.TransitionBackFromAnimation();
        DetermindNextState(controllerReference.meleeWeaponAttackTwo);
    }
}
