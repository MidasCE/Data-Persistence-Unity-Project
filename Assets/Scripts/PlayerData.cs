using System;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    
    
    public int HighestScore { get; private set; }
    public string HighestScorePlayerName { get; private set; }
    
    public string PlayerName { get; set; }
    public int PlayerScore { get; set; }
    
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnGameOver(int gameOverScore)
    {
        PlayerScore = gameOverScore;
        if (PlayerScore > HighestScore)
        {
            SaveNewPlayerScore();
        }
    }

    private void SaveNewPlayerScore()
    {
        HighestScore = PlayerScore;
        HighestScorePlayerName = PlayerName;
            
        SaveData data = new SaveData();
        data.HighestScore = PlayerScore;
        data.HighestScorePlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);
  
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    
    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighestScore = data.HighestScore;
            HighestScorePlayerName = data.HighestScorePlayerName;
        }
        else
        {
            HighestScore = 0;
            HighestScorePlayerName = "NaN";
        }

        PlayerScore = 0;
        PlayerName = "NaN";
    }
}
