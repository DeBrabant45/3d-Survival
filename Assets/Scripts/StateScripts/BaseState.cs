using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected AgentController controllerReference;

    public virtual void EnterState(AgentController controller)
    {
        this.controllerReference = controller;
    }

    public virtual void HandleMovement(Vector2 input) { }
    public virtual void HandleCameraDirection(Vector3 input) { }
    public virtual void HandleJumpInput() { }
    public virtual void HandleInventoryInput() { }
    public virtual void HandleHotBarInput(int hotbarKey) { }
    public virtual void Update() { }
    public virtual void HandleSecondaryInput() { }
    public virtual void HandlePrimaryInput() { }
    public virtual void HandleMenuInput() { }
}
