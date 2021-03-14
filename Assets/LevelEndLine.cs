using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndLine : MonoBehaviour
{
    bool _isLevelEnd;
    public bool IsLevelEnd { get => _isLevelEnd; set => _isLevelEnd = value; }
    GameData _gameData;
    LevelController _levelController;
    int i = 0;
    private void Awake()
    {
        _gameData = GameData.Instance;
        _levelController = LevelController.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Player")
        {
            gameObject.SetActive(false);
            _isLevelEnd = true;
            _gameData.IsLevelEnd = _isLevelEnd;
            _levelController.ChangeLevel();
        }
    }
}
