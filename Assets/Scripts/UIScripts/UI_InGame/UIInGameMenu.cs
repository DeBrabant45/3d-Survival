using AD.General;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameMenu : MonoBehaviour
{
    [SerializeField] private Button _saveBtn; 
    [SerializeField] private Button _exitBtn; 
    [SerializeField] private GameObject _gameMenuPanel; 
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameManager _gameManager;

    public bool IsMenuVisible { get => _gameMenuPanel.activeSelf; }

    private void Start()
    {
        _saveBtn.onClick.AddListener(_gameManager.SaveGame);
        _exitBtn.onClick.AddListener(_gameManager.ExitToMainMenu);
        _gameMenuPanel.SetActive(false);
    }

    public void ToggleMenu()
    {
        _gameMenuPanel.SetActive(!_gameMenuPanel.activeSelf);
    }

    public void ToggleLoadPanel()
    {
        _loadingPanel.SetActive(!_loadingPanel.activeSelf);
    }
}
