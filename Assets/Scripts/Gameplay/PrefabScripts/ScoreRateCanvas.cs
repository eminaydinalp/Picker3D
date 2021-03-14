using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRateCanvas : MonoBehaviour
{
    [SerializeField]
    Text winScore;
    [SerializeField]
    Text requireScore;

    GameData gameData;

	private void Awake()
	{
		gameData = GameData.Instance;
	}
	public void ScoreRate()
	{
		winScore.text = gameData.CollectedObjectCount.ToString() + "     /";
		if (gameData.GameMode == GameMode.Normal)
		{
			requireScore.text = gameData.CollectedObjectCountForPassLevel.ToString();
		}
		else
		{
			requireScore.text = gameData.CollectedObjectCountForPassLevelForInfinity.ToString();
		}
	}
	public void EnableScoreRate()
	{
		winScore.enabled = true;
		requireScore.enabled = true;
	}
	public void UnEnableScoreRate()
	{
		winScore.enabled = false;
		requireScore.enabled = false;
	}
}
