using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.IsMeleeWeaponDefenseAnimationActive(true);
        controllerReference.BlockObject.SetActive(true);
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.Movement.HandleMovement(input);
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.BlockObject.SetActive(false);
        controllerReference.TransitionToState(controllerReference.meleeWeaponAimState);
        controllerReference.AgentAnimations.IsMeleeWeaponDefenseAnimationActive(false);
    }

    public override void Update()
    {
        base.Update();
        controllerReference.DetectionSystem.PreformDetection(controllerReference.InputFromPlayer.MovementDirectionVector);
        HandleMovement(controllerReference.InputFromPlayer.MovementInputVector);
        HandleCameraDirection(controllerReference.InputFromPlayer.MovementDirectionVector);
    }

}
