using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager
{
    public static LevelsManager Instance = new LevelsManager();

    public int Level { get; set; } = -1;
    public int UnLockLevel { get; set; }

    public bool IsTutorial => Level == -1;

    public void NextLevel()
    {
        UnLockLevel++;
        Level++;
    }

    public void LoadNextLevelScene()
    {
        NextLevel();
        LoadLevelScene();
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
