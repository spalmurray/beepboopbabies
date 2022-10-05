using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        ScoreManager.Instance.NotifyScoreUpdate += UpdateScoreText;
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.NotifyScoreUpdate -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
