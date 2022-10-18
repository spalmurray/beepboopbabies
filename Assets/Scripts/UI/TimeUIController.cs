using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeUIController : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    public Image image_Fill;//CountDown bar
    public Light light;//light for sunset

    private void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        ScoreManager.Instance.NotifyTimeUpdate += UpdateTimeText;
    }
    private void Update()
    {
        //Realtime CountDown bar
        image_Fill.fillAmount = ScoreManager.Instance.CurrentTime / ScoreManager.Instance.AllTime;
        //Real time light change
        light.intensity = ScoreManager.Instance.CurrentTime / ScoreManager.Instance.AllTime;
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.NotifyTimeUpdate -= UpdateTimeText;
    }

    private void UpdateTimeText(int score)
    {
        timeText.text = "Time: " + score;
    }
}
