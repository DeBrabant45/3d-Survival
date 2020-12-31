using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.GameManager.ToggleGameMenu();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public override void HandleMenuInput()
    {
        base.HandleMenuInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        controllerReference.GameManager.ToggleGameMenu();
        controllerReference.TransitionToState(controllerReference.PreviousState);
    }
}
