using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpdate : MonoBehaviour
{
    public static CharacterUpdate Instance;
    public Color[] colors;
    public int index = -1;
    public int CharacterNum = 0;
    private void Awake()
    {
        Instance = this;
    }
    public Color GetColor()
    {
        index++;
        return colors[index];
    }
}
