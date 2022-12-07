using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager
{
    public static LevelsManager Instance = new LevelsManager();

    public int Level { get; set; }

    public int UnlockedLevel
    {
        get => PlayerPrefs.GetInt("UnlockedLevel", 0);
        set => PlayerPrefs.SetInt("UnlockedLevel", value);
    }

    public bool IsTutorial => Level == 0;

    private LevelsManager()
    {
        Level = UnlockedLevel;
    }

    public void LoadLevelScene()
    {
        if (IsTutorial)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Scene1");
        }
    }
}
