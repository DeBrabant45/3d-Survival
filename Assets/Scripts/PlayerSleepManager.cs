using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleepManager : MonoBehaviour
{
    [SerializeField] private UIPlayerSleep _playerSleepUI;
    [SerializeField, Range(0.1f, 1)] private float _timeModifier = 1f;

    public void Start()
    {
        _playerSleepUI = GetComponent<UIPlayerSleep>();
    }

    public void ShowUI()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _playerSleepUI.Show();
        Debug.Log("Freeze time");
    }

    public void SaveBed()
    {
        FindObjectOfType<AgentController>().SaveSpawnPoint();
    }

    public void ExitUI()
    {
        _playerSleepUI.Hide();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PlayerSleep()
    {
        _playerSleepUI.ToggleAllButtons();
        StartCoroutine(PlayerSleepCoroutine(4));
    }

    IEnumerator PlayerSleepCoroutine(int seconds)
    {
        for (int i = 1; i <= seconds; i++)
        {
            yield return new WaitForSecondsRealtime(_timeModifier);
            Debug.Log("Slept "  + i + "/" + seconds + "hours");
        }
        Debug.Log("Respawn Items");
        Debug.Log("Restore player states");
        _playerSleepUI.ToggleAllButtons();
    }
}
