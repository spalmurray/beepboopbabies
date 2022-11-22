using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBaby : MonoBehaviour
{
    public AudioClip audio;
    bool isPlay = false;
    bool isFinish = false;
    bool isCharge = false;
    void Update()
    {
        if (transform.childCount != 0)
        {
            //Debug.Log(transform.GetChild(0).GetComponent<BabyState>().currentHealth);
            if (transform.GetChild(0).GetComponent<BabyState>().currentEnergy < 100 && isPlay == false)
            {
                isCharge = true;
                //Play
                isPlay = true;
                GetComponent<AudioSource>().Play();
            }
            if (transform.GetChild(0).GetComponent<BabyState>().currentEnergy >= 95 && isFinish == false&& isCharge)
            {
                isFinish = true;
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(audio);
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            isPlay = false;
            isFinish = false;
            isCharge = false;
        }
    }
}
