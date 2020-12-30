using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.InventorySystem.ToggleInventory();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public override void HandleInventoryInput()
    {
        base.HandleInventoryInput();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controllerReference.InventorySystem.ToggleInventory();
        controllerReference.TransitionToState(controllerReference.movementState);
    }
}
