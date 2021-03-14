using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
	[SerializeField]
	Player _player;

	#region Canvases
	[SerializeField]
	FinishGameCanvas _finishGameCanvas;

	[SerializeField]
	GameOverCanvas _gameoverCanvas;

	[SerializeField]
	ScoreRateCanvas _scoreRateCanvas;
	#endregion

	#region Levels
	[SerializeField]
	Level[] _levels;
	public Level[] Levels { get => _levels; }

	[SerializeField]
	LevelEndObject[] _levelEndObjects;
	#endregion

	#region Variable	
	int _one = 0;
	int i = 0;
	int _cache;
	#endregion

	GameData _gameData;
	DataManager _dataManager;
	PlayerData _playerData;

	private void Start()
	{
		_gameData = GameData.Instance;
		_dataManager = FindObjectOfType<DataManager>();
        PlayerData playerData = _dataManager.ReadJsonFile();
        _gameData.CurrentLevel = playerData.level;
        _playerData = new PlayerData();
    }
	public void ChangeLevel()
	{
        if (_gameData.GameMode == GameMode.Normal)
        {
			if (_gameData.IsLevelEnd && _one == 0 && _gameData.IsStart && _gameData.CurrentLevel != 10)
			{
				_one++;
				_player.SetKinematic(true);
				StartCoroutine(ChangeLevelEnumerator());
			}
		}
        else
        {
			if (_gameData.IsLevelEnd && _one == 0 && _gameData.IsStart)
			{
				_one++;
				_player.SetKinematic(true);
				StartCoroutine(ChangeLevelEnumeratorForInfinity());
			}
		}
		
	}
	public void SetLevelPrefabs()
	{
		_levelEndObjects[_gameData.CurrentLevel].LevelEndLine.gameObject.SetActive(true);
		_levels[_gameData.CurrentLevel].SetCollectingObjects();
		foreach (var item in _levels[_gameData.CurrentLevel].CollectedObjects)
		{
			item.SetDefaultLocation();
		}
	}

	public void SetLevelPrefabsForInfinity()
	{
		_levelEndObjects[_gameData.InfinityLevel].LevelEndLine.gameObject.SetActive(true);
		//_levels[_gameData.InfinityLevel].SetCollectingObjects();
		foreach (var item in _levels[_gameData.InfinityLevel].CollectedObjects)
		{
			item.SetDefaultLocation();
		}
	}
	IEnumerator ChangeLevelEnumerator()
	{
		yield return new WaitForSeconds(2f);
		if (_gameData.CollectedObjectCount >= _gameData.CollectedObjectCountForPassLevel && _gameData.CurrentLevel != 9)
		{
			_gameData.IsLevelEnd = false;
			_levelEndObjects[_gameData.CurrentLevel].Open();
			_gameData.CurrentLevel++;
			FindObjectOfType<GameplayCanvas>().IncreaseLevel(_gameData.CurrentLevel);
			yield return new WaitForSeconds(.5f);
			_scoreRateCanvas.EnableScoreRate();
			_scoreRateCanvas.ScoreRate();
			yield return new WaitForSeconds(0.5f);
			_scoreRateCanvas.UnEnableScoreRate();
			_player.SetKinematic(false);
			_one = 0;
			_gameData.Score += _gameData.CollectedObjectCount;
		}
		else if (_gameData.CollectedObjectCount >= _gameData.CollectedObjectCountForPassLevel && _gameData.CurrentLevel == 9)
		{
			_one = 0;
			FindObjectOfType<GameplayCanvas>().IncreaseLevel(_gameData.CurrentLevel);
			_levelEndObjects[_gameData.CurrentLevel].Open();

			yield return new WaitForSeconds(.5f);
			_scoreRateCanvas.EnableScoreRate();
			_scoreRateCanvas.ScoreRate();
			yield return new WaitForSeconds(0.5f);
			_scoreRateCanvas.UnEnableScoreRate();

			_finishGameCanvas.gameObject.SetActive(true);
			_player.SetKinematic(true);
		}
		else
		{
			_scoreRateCanvas.EnableScoreRate();
			_scoreRateCanvas.ScoreRate();
			yield return new WaitForSeconds(0.5f);
			_scoreRateCanvas.UnEnableScoreRate();
			_gameoverCanvas.gameObject.SetActive(true);
			_gameoverCanvas.GameOver();
			_one = 0;
		}
		_gameData.CollectedObjectCount = 0;
        _playerData.level = _gameData.CurrentLevel;
        _playerData.score = _gameData.Score;
        _dataManager.Save(_playerData);
    }
	IEnumerator ChangeLevelEnumeratorForInfinity()
	{
		yield return new WaitForSeconds(2f);
		if (_gameData.CollectedObjectCount >= _gameData.CollectedObjectCountForPassLevelForInfinity)
		{
			_scoreRateCanvas.EnableScoreRate();
			_scoreRateCanvas.ScoreRate();
			yield return new WaitForSeconds(1f);
			_scoreRateCanvas.UnEnableScoreRate();
			_gameData.IsLevelEnd = false;
			_levelEndObjects[_gameData.InfinityLevel].Open();
			Debug.Log("InfinityLevel :" + +_gameData.InfinityLevel);
			SetLevelPrefabForInfinity();
			yield return new WaitForSeconds(.5f);
			_player.SetKinematic(false);
			_one = 0;
			_gameData.Score += _gameData.CollectedObjectCount;
			_gameData.CollectedObjectCount = 0;
		}
		else
		{
			_scoreRateCanvas.EnableScoreRate();
			_scoreRateCanvas.ScoreRate();
			yield return new WaitForSeconds(0.5f);
			_scoreRateCanvas.UnEnableScoreRate();
			_gameData.CollectedObjectCount = 0;
			_gameoverCanvas.gameObject.SetActive(true);
			_gameoverCanvas.GameOverForInfinity(_gameData.InfinityLevel);
			_one = 0;
		}
	}
	public void SetInfinityMode()
	{
		_gameData.IsLevelEnd = false;
		_player.SetKinematic(false);
		StartCoroutine(SetLevelPrefabForInfinityEnumeratorForFirst());
	}	
	public IEnumerator SetLevelPrefabForInfinityEnumeratorForFirst()
    {
		int randomLevel = Random.Range(0, 9);
		_gameData.InfinityLevel = randomLevel;
		_levels[randomLevel].gameObject.transform.localPosition = _levels[9].gameObject.transform.localPosition + new Vector3(0, 0, -155);
		int randomLevel1 = Random.Range(0, 9);
		_levels[randomLevel1].gameObject.transform.localPosition = _levels[randomLevel].gameObject.transform.localPosition + new Vector3(0, 0, -155);
		_levelEndObjects[randomLevel].SetActive();
		_levelEndObjects[randomLevel1].SetActive();
		_cache = randomLevel1;
		yield return new WaitForSeconds(3f);
		_player.SetKinematic(false);
	}
	public void SetLevelPrefabForInfinity()
	{
		int randomLevel = Random.Range(0, 9);
		Debug.Log("cache :" + +_cache);
		for (int i = 0; i < 100; i++)
        {
			if (_gameData.InfinityLevel == randomLevel || randomLevel == _cache )
            {
				randomLevel = Random.Range(0, 9);
            }
            else 
			{
				break;
			}
        }
		_gameData.InfinityLevel = _cache;
		Debug.Log("random :" + +randomLevel);

		_levels[randomLevel].gameObject.transform.localPosition = _levels[_cache].gameObject.transform.localPosition + new Vector3(0, 0, -155);
		_levels[randomLevel].SetCollectingObjects();
        _levelEndObjects[randomLevel].SetActive();
        _cache = randomLevel;
        _player.SetKinematic(false);
	}
	
	//public IEnumerator SetLevelPrefabForInfinityEnumerator()
	//{
	//	_levelEndObjects[_gameData.InfinityLevel].Open();
	//}
}
