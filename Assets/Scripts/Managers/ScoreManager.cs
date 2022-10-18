using System.Collections;
using UnityEngine;

public delegate void ScoreUpdate(int score);
public delegate void TimeUpdate(int time);
public delegate void GameOverHandler();

public class ScoreManager : MonoBehaviour
{
    public GameObject Image;//Clock UI
    public AudioClip audioClip;//Audio for Clock
    private int score;
    private float CurrentTime = 120;//CountDown
    private float AllTime;//Toal time for CountDown
    
    public static ScoreManager Instance 
    { 
        get; private set; 
    }

    public float CurrentTime1 
    { 
        get => CurrentTime; set => CurrentTime = value; //CountDown bar uasage
    }

    public float AllTime1 
    { 
        get => AllTime; set => AllTime = value; //CountDown bar uasage
    }

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        Instance = this;
        AllTime = CurrentTime;
    }
    private void Update()
    {
        UpdateTime();
        if (CurrentTime < 0 && !IsGameOver)
        {
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        Time.timeScale = 0f;
        Image.SetActive(true);
        Image.GetComponent<AudioSource>().PlayOneShot(audioClip);
        IsGameOver = true;
        
        yield return new WaitForSecondsRealtime(2);
        
        Image.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
        HandleGameOver?.Invoke();
    }

    // Event to notify that score has updated
    public event ScoreUpdate NotifyScoreUpdate;
    public event TimeUpdate NotifyTimeUpdate;
    public event GameOverHandler HandleGameOver;

    public int GetScore()
    {
        return score;
    }

    public void UpdateScore(int delta)
    {
        score += delta;
        NotifyScoreUpdate?.Invoke(score);
    }
    public void UpdateTime()
    {
        CurrentTime -= Time.deltaTime;
        NotifyTimeUpdate?.Invoke((int)CurrentTime);
    }
}