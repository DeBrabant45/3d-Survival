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

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("LoadSavedData") == 1)
        {
            StartCoroutine(_saveSystem.LoadSavedDataCoroutine(DoneLoading));
            PlayerPrefs.SetInt("LoadSavedData", 0);
        }
    }

    private void DoneLoading()
    {
        Debug.Log("Data loaded");
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void StartNextScene()
    {
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
