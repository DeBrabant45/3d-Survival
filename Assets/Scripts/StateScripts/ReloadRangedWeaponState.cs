using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadRangedWeaponState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        Debug.Log(controllerReference.AmmoSystem.IsAmmoAvailable());
        controllerReference.TransitionToState(controllerReference.movementState);
    }
}
