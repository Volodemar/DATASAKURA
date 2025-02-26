using UnityEngine;

public class GameData
{
	public LevelData LevelData = new LevelData();

	public void Save()
	{
		Debug.Log("GameData is Save...");
	}

	public void Load()
	{
		Debug.Log("GameData is Load...");
	}

	public void Reset()
	{
		LevelData.Reset();
	}
}
