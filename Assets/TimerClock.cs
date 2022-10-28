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
    }

    // Update is called once per frame
    void Update()
    {
        if (timeon) {
            if (timeleft > 0) {
                timeleft -= Time.deltaTime;
                updateTimer(timeleft);
            } else {
                Debug.Log("time is up");
                timeleft = 0;
                timeon = false;
            }
        }
    }

    void updateTimer(float CurrentTime) {
        CurrentTime += 1;
        float min = Mathf.FloorToInt(CurrentTime / 60);
        float sec = Mathf.FloorToInt(CurrentTime % 60);
        TimerTxt.text = string.Format("{0:00} : {1:00}", min, sec);
    }
}
