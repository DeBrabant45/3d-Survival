using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : BaseState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.GameManager.ToggleGameMenu();
        controllerReference.GameManager.AudioManager.PauseAllMapSounds();
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
        controllerReference.GameManager.AudioManager.StartAllMapSounds();
        controllerReference.TransitionToState(controllerReference.PreviousState);
    }
}
