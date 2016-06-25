﻿using UnityEngine;
using System.IO;

using System.Collections.Generic;
using System;

using System.Runtime.Serialization;
using System.Xml.Serialization;

#if UNITY_STANDALONE_OSX
using System.Runtime.Serialization.Formatters.Binary;
#endif

public class GamePlayerDataController {

	string playerDataFileName;

    public bool playerExist { get; set; }
    public float gameMusicVolume { get; set; }
    public float gameSoundEffectsVolume { get; set; }
    public string playerName { get; set; }

    public int completedLevelsCount { get; set; }
    public int playerScore { get; set; }
    public int selectedLevelIndex { get; set; }
    public int completedTutorialsCount { get; set; }
    public bool showReviewSuggestion { get; set; }

    public int playerStartLevelScore { get; set; }
	public bool selectEndlessLevel { get; set; }
	public int endlessLevelPlayedTime { get; set; }

    string _dataPath;

    List<LevelSpell> _playerSpells;
    List<string> _previousScenes;

    public GamePlayerDataController()
    {
		playerDataFileName = getPlayerDataFileName();
        _dataPath = Application.persistentDataPath + playerDataFileName;
        _previousScenes = new List<string>();
        loadPlayerData();
        selectedLevelIndex = 0;
    }

	string getPlayerDataFileName()
	{
		string fileName = "/playerData.xml";

		#if UNITY_STANDALONE_OSX
		fileName = "/playerData.bt";
		#endif

		return fileName;
	}

	IFormatter getDataFormatter()
	{
		IFormatter formatter = new PlayerDataXMLFormatter();

		#if UNITY_STANDALONE_OSX
		formatter = new BinaryFormatter();
		#endif

		return formatter;
	}

    void loadPlayerData()
    {
        if(File.Exists(_dataPath))
        {
			IFormatter formatter = getDataFormatter();
            FileStream file = File.Open(_dataPath, FileMode.Open);
            PlayerData data = formatter.Deserialize(file) as PlayerData;

            playerExist = data.playerExist;
            gameMusicVolume = data.gameMusicVolume;
            gameSoundEffectsVolume = data.gameSoundEffectsVolume;
            playerName = data.playerName;
            completedLevelsCount = data.completedLevelsCount;
            playerScore = data.playerScore;
			endlessLevelPlayedTime = data.endlessLevelPlayedTime;
            _playerSpells = data.playerSpells;
            completedTutorialsCount = data.completedTutorialsCount;
            showReviewSuggestion = data.showReviewSuggestion;

            if (_playerSpells == null)
            {
                _playerSpells = new List<LevelSpell>();
            }

            file.Close();
        }
    }

    public void savePlayerData()
    {
		IFormatter formatter = getDataFormatter();
        FileStream file = File.Create(_dataPath);
        PlayerData savingData = new PlayerData();

        savingData.playerExist = playerExist;
        savingData.gameMusicVolume = gameMusicVolume;
        savingData.gameSoundEffectsVolume = gameSoundEffectsVolume;
        savingData.playerName = playerName;
        savingData.completedLevelsCount = completedLevelsCount;
        savingData.playerScore = playerScore;
		savingData.endlessLevelPlayedTime = endlessLevelPlayedTime;
        savingData.playerSpells = _playerSpells;
        savingData.completedTutorialsCount = completedTutorialsCount;
        savingData.showReviewSuggestion = showReviewSuggestion;

        formatter.Serialize(file, savingData);
        file.Close();
    }

    public void createNewPlayerWithName(string aPlayerName)
    {
        playerExist = true;
        playerName = aPlayerName;
        completedLevelsCount = 0;
        playerScore = 0;
		endlessLevelPlayedTime = 0;
        completedTutorialsCount = 0;
        _playerSpells = new List<LevelSpell>();
        selectedLevelIndex = 0;
        completedTutorialsCount = 0;
        showReviewSuggestion = false;
        savePlayerData();
    }

    public void cleanPlayer()
    {
        playerExist = false;
        playerName = "";
        completedLevelsCount = 0;
        playerScore = 0;
		endlessLevelPlayedTime = 0;
        _playerSpells = new List<LevelSpell>();
        gameMusicVolume = 0.5f;
        gameSoundEffectsVolume = 0.5f;
        selectedLevelIndex = 0;
        completedTutorialsCount = 0;
        savePlayerData();
    }

    public int getPlayerSpellsCount()
    {
        return _playerSpells.Count;
    }

    public void setPlayerSpell(LevelSpell aSpellData)
    {
        if (aSpellData.spellType > (getPlayerSpellsCount() - 1))
        {
            _playerSpells.Add(aSpellData);
        }
        else
        {
            _playerSpells[aSpellData.spellType] = aSpellData;
        }
    }

    public LevelSpell getPlayerSpellForIndex(int aSpellIndex)
    {
        return _playerSpells[aSpellIndex];
    }

    public void pushCurrentSceneName(string aSceneName)
    {
        _previousScenes.Add(aSceneName);
    }

    public string popPreviousScene()
    {
        string previousSceneName = _previousScenes[_previousScenes.Count - 1];
        _previousScenes.RemoveAt(_previousScenes.Count - 1);
        return previousSceneName;
    }

}

[Serializable]
public class PlayerData
{
    public bool playerExist;
    public float gameMusicVolume;
    public float gameSoundEffectsVolume;
    public string playerName;
    public int completedLevelsCount;
    public int playerScore;
	public int endlessLevelPlayedTime;
    public List<LevelSpell> playerSpells;
    public int completedTutorialsCount;
    public bool showReviewSuggestion;
}

public class PlayerDataXMLFormatter: IFormatter
{
	public ISurrogateSelector SurrogateSelector { get; set; }
	public SerializationBinder Binder { get; set; }
	public StreamingContext Context { get; set; }

	public void Serialize(Stream serializationStream, object graph)
	{
		XmlSerializer formatter = new XmlSerializer(typeof(PlayerData));
		formatter.Serialize(serializationStream, graph);
	}

	public object Deserialize(Stream serializationStream)
	{
		XmlSerializer formatter = new XmlSerializer(typeof(PlayerData));
		PlayerData data = formatter.Deserialize(serializationStream) as PlayerData;
		return data;
	}
}
