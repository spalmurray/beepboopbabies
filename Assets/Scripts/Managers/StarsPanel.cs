using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsPanel : MonoBehaviour
{
    public static StarsPanel Instance;
    private float StarsScore = 0;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        //if (StarsScore > 0 && StarsScore <= 0.5)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
        //}
        //else if (StarsScore > 0.5f && StarsScore <= 1)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
        //}
        //else if (StarsScore > 1f && StarsScore <= 1.5f)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(1).GetComponent<Image>().fillAmount = 0.5f;
        //}
        //else if (StarsScore > 1.5f && StarsScore <= 2f)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
        //}
        //else if (StarsScore > 2f && StarsScore <= 2.5f)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(2).GetComponent<Image>().fillAmount = 0.5f;
        //}
        //else if (StarsScore > 2.5f && StarsScore <= 3f)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(2).GetComponent<Image>().fillAmount = 1f;
        //}
        //else if (StarsScore > 3f && StarsScore <= 3.5f)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(2).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(3).GetComponent<Image>().fillAmount = 0.5f;
        //}
        //else if (StarsScore > 3.5f && StarsScore <= 4f)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(2).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(3).GetComponent<Image>().fillAmount = 1f;
        //}
        //else if (StarsScore > 4f && StarsScore <= 4.5f)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(2).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(3).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(4).GetComponent<Image>().fillAmount = 0.5f;
        //}
        //else if (StarsScore > 4.5f)
        //{
        //    transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(1).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(2).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(3).GetComponent<Image>().fillAmount = 1f;
        //    transform.GetChild(4).GetComponent<Image>().fillAmount = 1f;
        //}
    }
    //public void AddStarsScore(float score)
    //{
    //    StarsScore += score;
    //}
    public void ShowStars(float t1, float t2, float t3, float t4, float t5)
    {
        float[] ts = new float[] { t1, t2, t3, t4, t5 };
        for (int i = 0; i < ts.Length - 1; i++)
        {
            for (int j = 0; j < ts.Length - 1 - i; j++)
            {
                if (ts[j] > ts[j + 1]) 
                {
                    float temp = ts[j + 1]; 
                    ts[j + 1] = ts[j];
                    ts[j] = temp; 
                }
            }
        }
        //transform.GetChild(0).GetComponent<Image>().fillAmount = ts[0];
        for (int i = 0; i < 5; i++)
        {
            transform.GetChild(4 - i).GetComponent<Image>().fillAmount = ts[i];
        }
    }
}
