using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttackThreeState : MeleeState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
    }

    public override void TransitionBackFromAnimation()
    {
        base.TransitionBackFromAnimation();
        controllerReference.TransitionToState(controllerReference.meleeWeaponAimState);
    }
}
