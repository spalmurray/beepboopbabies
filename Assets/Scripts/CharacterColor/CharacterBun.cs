using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBun : MonoBehaviour
{
    private void OnEnable()
    {
        switch (CharacterUpdate .Instance .CharacterNum)
        {
            case 0:
                Show(PlayerPrefs.GetInt("Player1"));
                break;
            case 1:
                Show(PlayerPrefs.GetInt("Player2"));
                break;
            case 2:
                Show(PlayerPrefs.GetInt("Player3"));
                break;
            case 3:
                Show(PlayerPrefs.GetInt("Player4"));
                break;
            default:
                break;
        }
        CharacterUpdate.Instance.CharacterNum++;
    }
    private void Show(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            transform .GetChild(i).gameObject.SetActive(false);
        }
        transform .GetChild(index).gameObject.SetActive(true);
    }
}
