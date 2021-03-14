using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{    
    [SerializeField]
    TextAsset jsonPlayerr;
    [SerializeField]
    TextMeshProUGUI _scoreText;
    [SerializeField]
    Button _restartButton;
    [SerializeField]
    GameplayCanvas _gameplayCanvas;
    [SerializeField]
    StartCanvas _startCanvas;
    GameData _gameData;
    GameController _gameController;
    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameData = GameData.Instance;       
        _restartButton.onClick.AddListener(RestartGame);

    }
    public void GameOver()
    {
        _gameData.IsStart = false;
        _gameData.IsLevelEnd = false;
        _gameData.Score = 0;
        _scoreText.text = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>().text;
        _gameController.SetSceneForGameover();       
    }
    public void GameOverForInfinity(int cache)
    {
        _gameData.IsStart = false;
        _gameData.IsLevelEnd = false;
        _gameData.Score = 0;
        _scoreText.text = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>().text;
        _gameController.SetSceneForInfinityForGameOver(cache);

    }
    void RestartGame()
    {
        gameObject.SetActive(false);
        _startCanvas.gameObject.SetActive(false);
        _gameplayCanvas.gameObject.SetActive(true);
        _gameplayCanvas.Countdown();
    }
    
}
