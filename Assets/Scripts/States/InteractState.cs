using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        Debug.Log("Interaction State");
    }

    public override void HandlePrimaryInput()
    {
        base.HandlePrimaryInput();
    }

    public override void HandleSecondaryInput()
    {
        base.HandleSecondaryInput();
    }

    public override void Update()
    {
        base.Update();
        controllerReference.TransitionToState(controllerReference.movementState);
    }
}
