using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishGameCanvas : MonoBehaviour
{
    [SerializeField]
    Button _continueButton;
    [SerializeField]
    Button _mainMenuButton;
    [SerializeField]
    TextMeshProUGUI _scoreText;
    GameController _gameController;
    private void Awake()
    {
        _gameController = GameController.Instance;
        _continueButton.onClick.AddListener(ContinueGame);
        _mainMenuButton.onClick.AddListener(GoMainMenu);
    }

    void ContinueGame()
    {
        FindObjectOfType<GameData>().GameMode = GameMode.Infinity;
        gameObject.SetActive(false);
        _gameController.SetSceneForInfinity();
    }

    void GoMainMenu()
    {

    }
}

