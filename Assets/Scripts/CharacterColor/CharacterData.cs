using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterData : MonoBehaviour
{
    public Transform Player1;
    public Transform Player2;
    public Transform Player3;
    public Transform Player4;

    private int index1 = 0;
    private int index2 = 0;
    private int index3 = 0;
    private int index4 = 0;

    public void SetCharact()
    {
        for (int i = 0; i < 4; i++)
        {
            Player1.GetChild(i).gameObject.SetActive(false);
            Player2.GetChild(i).gameObject.SetActive(false);
            Player3.GetChild(i).gameObject.SetActive(false);
            Player4.GetChild(i).gameObject.SetActive(false);
        }
        Player1.GetChild(index1).gameObject.SetActive(true);
        Player2.GetChild(index2).gameObject.SetActive(true);
        Player3.GetChild(index3).gameObject.SetActive(true);
        Player4.GetChild(index4).gameObject.SetActive(true);
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("Player1", index1);
        PlayerPrefs.SetInt("Player2", index2);
        PlayerPrefs.SetInt("Player3", index3);
        PlayerPrefs.SetInt("Player4", index4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Btn1(bool IsLeft)
    {
        if (IsLeft)
        {
            if (index1 == 0)
            {
                index1 = 3;
                SetCharact();
                return;
            }
            index1--;
            SetCharact();
        }
        else
        {
            if (index1 == 3)
            {
                index1 = 0;
                SetCharact();
                return;
            }
            index1++;
            SetCharact();
        }
    }
    public void Btn2(bool IsLeft)
    {
        if (IsLeft)
        {
            if (index2 == 0)
            {
                index2 = 3;
                SetCharact();
                return;
            }
            index2--;
            SetCharact();
        }
        else
        {
            if (index2 == 3)
            {
                index2 = 0;
                SetCharact();
                return;
            }
            index2++;
            SetCharact();
        }
    }
    public void Btn3(bool IsLeft)
    {
        if (IsLeft)
        {
            if (index3 == 0)
            {
                index3 = 3;
                SetCharact();
                return;
            }
            index3--;
            SetCharact();
        }
        else
        {
            if (index3 == 3)
            {
                index3 = 0;
                SetCharact();
                return;
            }
            index3++;
            SetCharact();
        }
    }
    public void Btn4(bool IsLeft)
    {
        if (IsLeft)
        {
            if (index4 == 0)
            {
                index4 = 3;
                SetCharact();
                return;
            }
            index4--;
            SetCharact();
        }
        else
        {
            if (index4 == 3)
            {
                index4 = 0;
                SetCharact();
                return;
            }
            index4++;
            SetCharact();
        }
    }
}
