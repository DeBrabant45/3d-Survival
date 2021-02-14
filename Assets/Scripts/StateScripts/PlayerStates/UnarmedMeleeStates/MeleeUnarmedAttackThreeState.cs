using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnarmedAttackThreeState : MeleeState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.TriggerUnarmedMeleeAttackThreeAnimation();
    }

    public override void TransitionBackFromAnimation()
    {
        base.TransitionBackFromAnimation();
        DetermindNextState(controllerReference.meleeUnarmedAttackFour);
    }
}
