using UnityEngine;

public delegate void ScoreUpdate(int score);
public delegate void TimeUpdate(int time);

public class ScoreManager : MonoBehaviour
{
    public GameObject PauseCanvas;//End round UI
    public GameObject Image;//Clock UI
    public AudioClip audioClip;//Audio for Clock
    private int score;
    private float CurrentTime = 60;//CountDown
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
    
    private bool isTime = true;

    private void Awake()
    {
        Instance = this;
        AllTime = CurrentTime;
    }
    private void Update()
    {
        if (CurrentTime < 0)
        {
            //CountDown over, Show endUI and pause game
            if (isTime)
            {
                Image.SetActive(true);
                Image.GetComponent<AudioSource>().PlayOneShot(audioClip);
                isTime = false;
                Invoke("ShowPanel", 2);
            }
            return;
        }
        UpdateTime();
    }
    private void ShowPanel()
    {
        Time.timeScale = 0f;
        Image.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
        PauseCanvas.SetActive(true);

    }

    // Event to notify that score has updated
    public event ScoreUpdate NotifyScoreUpdate;
    public event TimeUpdate NotifyTimeUpdate;

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