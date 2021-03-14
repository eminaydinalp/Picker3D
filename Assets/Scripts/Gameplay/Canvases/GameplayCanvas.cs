using System.Collections;

using TMPro;

using UnityEngine;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoreText;
    [SerializeField]
    TextMeshProUGUI _levelText;
    [SerializeField]
    TextMeshProUGUI _countdownText;
    GameController _gameController;

    GameData _gameData;
    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameData = GameData.Instance;
    }
    public void Countdown()
    {
        _levelText.text = _gameData.CurrentLevel.ToString();
        if (_gameData.GameMode == GameMode.Normal)
        {
            StartCoroutine(CountDownEnumerator());
        }
        else
        {
            StartCoroutine(CountDownEnumeratorInfinity());
        }
    }
    IEnumerator CountDownEnumerator()
    {
        _gameController.SetScene();
        _countdownText.enabled = true;
        _countdownText.text = 3.ToString();
        yield return new WaitForSeconds(1);
        _countdownText.text = 2.ToString();
        yield return new WaitForSeconds(1);
        _countdownText.text = 1.ToString();
        yield return new WaitForSeconds(1);
        _countdownText.enabled = false;
        FindObjectOfType<Player>().SetKinematic(false);
    }
    IEnumerator CountDownEnumeratorInfinity()
    {
        _countdownText.enabled = true;
        _countdownText.text = 3.ToString();
        yield return new WaitForSeconds(1);
        _countdownText.text = 2.ToString();
        yield return new WaitForSeconds(1);
        _countdownText.text = 1.ToString();
        yield return new WaitForSeconds(1);
        _countdownText.enabled = false;
        _gameData.IsStart = true;
        FindObjectOfType<Player>().SetKinematic(false);
    }

    public void IncreaseScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void IncreaseLevel(int level)
    {
        _levelText.text = level.ToString();
    }
}
