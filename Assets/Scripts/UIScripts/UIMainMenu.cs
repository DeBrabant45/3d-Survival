using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button _newGameBtn;
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _newGameBtn.onClick.AddListener(_gameManager.StartNextScene);
        _resumeBtn.onClick.AddListener(_gameManager.LoadSavedGame);
        _resumeBtn.interactable = false;
        if(_gameManager.CheckedSavedGameExist())
        {
            _resumeBtn.interactable = true;
        }
    }
}
