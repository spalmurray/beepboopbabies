using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairBaby : MonoBehaviour
{
    bool isPlay = false;
    bool isFinish = false;
    void Update()
    {
        if (transform.childCount != 0)
        {
            //Debug.Log(transform.GetChild(0).GetComponent<BabyState>().currentHealth);
            if (transform.GetChild(0).GetComponent<BabyState>().currentHealth < 100 && isPlay == false)
            {
                //Play
                isPlay = true;
                GetComponent<AudioSource>().Play();
            }
            if (transform.GetChild(0).GetComponent<BabyState>().currentHealth >= 100 && isFinish == false)
            {
                isFinish = true;
                GetComponent<AudioSource>().Stop();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            isPlay = false;
            isFinish = false;
        }
    }
}
