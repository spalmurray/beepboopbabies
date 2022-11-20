using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerClock : MonoBehaviour
{
    public float timeleft;
    public bool timeon = false;
    public Text TimerTxt;

    void Start()
    {
        timeon = true;
        //TimerTxt.text = "8:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (timeon)
        {
            if (timeleft > 0)
            {
                timeleft += Time.deltaTime * 4;
                updateTimer(timeleft);
            }
            else
            {
                Debug.Log("time is up");
                timeleft = 0;
                timeon = false;
            }
        }
    }
    bool isPlay = false;
    void updateTimer(float CurrentTime)
    {
        CurrentTime += 1;
        float min = Mathf.FloorToInt(CurrentTime / 60);
        float sec = Mathf.FloorToInt(CurrentTime % 60);
        if (min + 6 <= 12)
            TimerTxt.text = string.Format("{0:00} : {1:00}", min + 6, sec);
        else
        {
            TimerTxt.text = string.Format("{0:00} : {1:00}", min + 6 - 12, sec);
            //Play Camera Shake
            if (min + 6 - 12 >= 4 && isPlay == false)
            {
                Debug.Log("Play Camera Shake");
                isPlay = true;
                EZCameraShake.CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
            }
        }

    }
}
