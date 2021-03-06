using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        var usableStructure = controllerReference.DetectionSystem.UsableCollider;
        if(usableStructure != null)
        {
            usableStructure.GetComponent<IUsable>().Use();
            return;
        }
        var resultCollider = controllerReference.DetectionSystem.CurrentCollider;
        if(resultCollider != null)
        {
            var ipickable = resultCollider.GetComponent<IPickable>();
            var remainder = controllerReference.InventorySystem.AddToStorage(ipickable.PickUp());
            ipickable.SetCount(remainder);
            if(remainder > 0)
            {
                Debug.Log("Can't pick it up");
            }
        }
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
