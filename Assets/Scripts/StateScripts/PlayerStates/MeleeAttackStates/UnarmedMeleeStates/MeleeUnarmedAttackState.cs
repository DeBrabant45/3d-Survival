using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnarmedAttackState : MeleeState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
    }

    public override void TransitionBackFromAnimation()
    {
        base.TransitionBackFromAnimation();
        DetermindNextState(controllerReference.meleeUnarmedAttackState, controllerReference.meleeUnarmedAim);
    }

    public override void PreformAttack(Collider hitObject)
    {
        var hittable = hitObject.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittable.GetHit(controllerReference.UnarmedAttack);
        }
    }
}
