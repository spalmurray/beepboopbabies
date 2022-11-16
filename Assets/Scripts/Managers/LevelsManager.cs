using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager
{
    public static LevelsManager Instance = new LevelsManager();

    public int Level { get; private set; }

    public void NextLevel()
    {
        Level++;
    }
}
