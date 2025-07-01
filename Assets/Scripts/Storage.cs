using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]

public class Storage : MonoBehaviour
{
    public static Storage Instance { get; private set; }
    public int money = 0;
    public float addBladePrice = 300;
    public float addEnemyPrice = 3;
    public float addSpeedPrice = 100;
    public float spawnInterval = 1;
    public int countOfBlades = 1;
    public int countOfMaces = 1;
    public float speedBlade = 2f;
    public float speedMace = 100f;
    public int addBladeLevel = 1;
    public int addEnemyLevel = 1;
    public int addSpeedLevel = 1;
    public float kills = 0;
    public float killsGoal = 5000;
    public int scene = 1;

    private string filePath;
    private BinaryFormatter formatter;

    private const string SaveKey = "game_data";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        }
        else
        {
            Destroy(gameObject);
        }
        Load();
    }
    private string GetPlayerSaveKey()
    {
        return $"{SaveKey}";
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(GetPlayerSaveKey(), json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(GetPlayerSaveKey()))
        {
            string json = PlayerPrefs.GetString(GetPlayerSaveKey());
            JsonUtility.FromJsonOverwrite(json, Instance);

        }
        else
        {

            ResetSave();
        }

    }

   
    public void ResetSave()
    {
        money = 0;
              addBladePrice = 300;
      addEnemyPrice = 3;
      addSpeedPrice = 100;
      spawnInterval = 1;
      countOfBlades = 1;
      countOfMaces = 1;
      speedBlade = 2f;
      speedMace = 100f;
      addBladeLevel = 1;
      addEnemyLevel = 1;
      addSpeedLevel = 1;
      kills = 0;
      killsGoal = 5000;
      scene = 1;

    Save();
    }
}