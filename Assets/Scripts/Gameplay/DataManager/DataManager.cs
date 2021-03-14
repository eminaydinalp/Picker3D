using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class DataManager : MonoBehaviour
{
	PlayerData playerData;
	private void Awake()
	{
		playerData = new PlayerData();
		CreateJsonFile();
	}
	public void CreateJsonFile()
	{
		string path = GetFilePath();
		if (!File.Exists(path))
		{
			Debug.Log("Dosya yok");
			FileStream fileStream = File.Create(path);
			fileStream.Close();
			Save(playerData);
		}
	}
	public void Save(PlayerData playerData)
	{
		string path = GetFilePath();
		string json = JsonUtility.ToJson(playerData);
		File.WriteAllText(path, json);
	}
	public PlayerData ReadJsonFile()
	{
		string path = GetFilePath();
		string json = File.ReadAllText(path);
		playerData = JsonUtility.FromJson(json, typeof(PlayerData)) as PlayerData;
		return playerData;
	}

	private string GetFilePath()
	{
		return Application.persistentDataPath + "/Playerdata.json";
	}

}
