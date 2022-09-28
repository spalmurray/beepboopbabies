using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ScoreUpdate(int score);

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Event to notify that score has updated
    public event ScoreUpdate NotifyScoreUpdate;

    private int score = 0;

    public int GetScore()
    {
        return score;
    }

    public void UpdateScore(int delta)
    {
        score += delta;
        NotifyScoreUpdate?.Invoke(score);
    }
}