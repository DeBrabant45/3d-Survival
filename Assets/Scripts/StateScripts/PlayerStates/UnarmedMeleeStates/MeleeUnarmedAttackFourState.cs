using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnarmedAttackFourState : MeleeState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.TriggerUnarmedMeleeAttackFourAnimation();
    }

    public override void TransitionBackFromAnimation()
    {
        base.TransitionBackFromAnimation();
        controllerReference.TransitionToState(controllerReference.meleeUnarmedAim);
    }
}
