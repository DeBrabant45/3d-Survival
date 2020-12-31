using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private string _mainMenuSceneName;
    [SerializeField] private UIInGameMenu _uIInGameMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("LoadSavedData") == 1)
        {
            Time.timeScale = 0;
            _uIInGameMenu.ToggleLoadPanel();
            StartCoroutine(_saveSystem.LoadSavedDataCoroutine(DoneLoading));
            PlayerPrefs.SetInt("LoadSavedData", 0);
        }
    }

    public void SaveGame()
    {
        _saveSystem.SaveObjects();
    }

    private void DoneLoading()
    {
        _uIInGameMenu.ToggleLoadPanel();
        Time.timeScale = 1;
    }

    public void ToggleGameMenu()
    {
        _uIInGameMenu.ToggleMenu();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void StartNextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadSavedGame()
    {
        PlayerPrefs.SetInt("LoadSavedData", 1);
        StartNextScene();
    }

    public bool CheckedSavedGameExist()
    {
        return _saveSystem.CheckSavedDataExists();
    }
}
