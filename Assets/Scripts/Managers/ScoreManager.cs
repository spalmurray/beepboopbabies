using UnityEngine;

public delegate void ScoreUpdate(int score);

public class ScoreManager : MonoBehaviour
{
    private int score;
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Event to notify that score has updated
    public event ScoreUpdate NotifyScoreUpdate;

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