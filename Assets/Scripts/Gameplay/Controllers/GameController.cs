using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField]
    Player _player;

    #region Canvases  
    [SerializeField]
    EndObject _endObject;
    #endregion

    #region Checkouts
    [SerializeField]
    Transform[] _checkouts;
    public Transform[] Checkouts { get => _checkouts; }

    #endregion

    [SerializeField]
    Level[] _levels;

    LevelController _levelController;
    GameData _gameData;

    private void Awake()
    {
        _levelController = LevelController.Instance;
        _gameData = GameData.Instance;  
    }
    public void SetScene()
    {
        _player.SetLocation();
        _gameData.IsStart = true;
    }
    public void SetSceneForGameover()
    {
        _player.SetLocation();
        _levelController.SetLevelPrefabs();      
    }


    #region Normal

    #endregion

    #region Infinity
    public void SetSceneForInfinity()
    {
        _endObject.Open();
        StartCoroutine(SetSceneForInfinityEnumerator());
    }
    public void SetSceneForInfinityForGameOver(int cache)
    {
        _player.SetLocationForInfinity(cache);
        _levelController.SetLevelPrefabsForInfinity();
    }
    IEnumerator SetSceneForInfinityEnumerator()
    {
        yield return new WaitForSeconds(.5f);
        _levelController.SetInfinityMode();
    }
    #endregion
}
