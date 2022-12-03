using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager
{
    public static LevelsManager Instance = new LevelsManager();

    public int Level { get; set; }
    public int UnLockLevel { get; set; }

    public void NextLevel()
    {
        UnLockLevel++;
        Level++;
    }
}
