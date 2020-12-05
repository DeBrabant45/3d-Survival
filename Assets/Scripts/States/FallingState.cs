using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : JumpState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.TriggerFallAnimation();
        controllerReference.Movement.SetCompletedJumpFalse();
    }

    public override void Update()
    {
        base.Update();
    }

}
