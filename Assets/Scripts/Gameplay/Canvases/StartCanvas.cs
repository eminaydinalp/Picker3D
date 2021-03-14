using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartCanvas : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _latestScore;
    [SerializeField]
    Button _startButton;
    [SerializeField]
    GameplayCanvas _gameplayCanvas;
    DataManager _dataManager;
    private void Start()
    {
        _dataManager = FindObjectOfType<DataManager>();
        PlayerData playerData = _dataManager.ReadJsonFile();
        _latestScore.text = playerData.score.ToString();
        _startButton.onClick.AddListener(StartGame);  
    }

    void StartGame()
    {
        gameObject.SetActive(false);
        _gameplayCanvas.gameObject.SetActive(true);
        _gameplayCanvas.Countdown();
    }
}
