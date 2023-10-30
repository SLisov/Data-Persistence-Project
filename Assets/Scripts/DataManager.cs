using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI bestScore;
    public TMP_InputField playerNameInput;

    public string bestPlayer;
    public string playerNameText;
    public int maxScore;
    public string bestScoreText;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        LoadNameAndScore();
        playerNameInput.text = bestPlayer;
        BestScore();
        bestScore.text = bestScoreText;

    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
        playerNameText = playerName.text;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else   
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int maxScore;
    }

    public void SaveNameAndScore()
    {
        SaveData data = new SaveData();
        data.playerName = bestPlayer;
        data.maxScore = maxScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.playerName;
            maxScore  = data.maxScore;
        }
    }

    public string BestScore()
    {
        return bestScoreText = "Best Score" + ": " + bestPlayer + " : " + maxScore.ToString();
    }
}
