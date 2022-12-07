using System;
using System.Collections;
using UnityEngine;
using System.Linq;

public delegate void TimeUpdate(int time);
public delegate void GameOverHandler();

public class ScoreManager : MonoBehaviour
{
    public GameObject Image;//Clock UI
    public AudioClip audioClip;//Audio for Clock
    public AudioSource AudioSource;//Audio for countdown
    private float totalStars;
    private int currentParents;

    public static ScoreManager Instance
    {
        get; private set;
    }

    public float CurrentTime { get; private set; } = 150f; //set total game length
    private float ParentReturnTime = 40f; //set parent return time
    public float AllTime { get; private set; }

    public bool IsGameOver { get; private set; }

    // Final score, which is the average of "stars" of each parent
    public float FinalScore => LevelsManager.Instance.IsTutorial ?
        totalStars :
        RoundToHalf(totalStars / FindObjectOfType<ParentSpawnManager>().NumberOfParents);

    private void Awake()
    {
        Instance = this;
        AllTime = CurrentTime;
    }
    
    private void Update()
    {
        UpdateTime();
        // TODO:
        if (CurrentTime < ParentReturnTime && !LevelsManager.Instance.IsTutorial)
        {
            // make each parent return to their kid
            ParentSpawnManager.Instance.ReturnParents();
        }
        if (CurrentTime <= 0 && !IsGameOver && !LevelsManager.Instance.IsTutorial)
        {
            GameObject.Find("babis level music").GetComponent<AudioSource>().enabled = false;
            //PlayerPrefs.SetFloat("music", 0);
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        IsGameOver = true;
        yield return new WaitForSecondsRealtime(0.5f);
        
        Time.timeScale = 0f;
        Image.SetActive(true);
        if (Image.GetComponent<AudioSource>().isPlaying == false) {
            Image.GetComponent<AudioSource>().PlayOneShot(audioClip);
        }

        yield return new WaitForSecondsRealtime(2);

        if (FinalScore >= 3.0 || LevelsManager.Instance.IsTutorial)
        {
            LevelsManager.Instance.UnlockedLevel = Mathf.Max(
                LevelsManager.Instance.Level + 1,
                LevelsManager.Instance.UnlockedLevel
            );
        }
        
        Image.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
        HandleGameOver?.Invoke();
    }

    public event TimeUpdate NotifyTimeUpdate;
    public event GameOverHandler HandleGameOver;

    private void UpdateTime()
    {
        CurrentTime -= Time.deltaTime;
        CurrentTime = Mathf.Max(0, CurrentTime);  // in case of overtime in tutorial
        NotifyTimeUpdate?.Invoke((int)CurrentTime);
    }

    public void RegisterPickedUpBaby(BabyState babyState)
    {
        var stars = new[]
            {
                (babyState.currentEnergy, babyState.energy),
                (babyState.currentHealth, babyState.health),
                (babyState.currentDiaper, babyState.diaper),
                (babyState.currentFun, babyState.fun),
                (babyState.currentOil, babyState.oil),
            }
            .Select(tuple => tuple.Item1 / tuple.Item2)
            .Select(RoundToHalf)
            .Sum();
        var numBodyPartsMissing = (babyState.health - babyState.healthcap) / 20;
        stars = Mathf.Max(stars - numBodyPartsMissing, 0);
        
        StarsPanel.Instance.ShowStars(stars);
        totalStars += stars;
        currentParents++;

        if (LevelsManager.Instance.IsTutorial || currentParents == FindObjectOfType<ParentSpawnManager>().NumberOfParents)
        {
            StartCoroutine(EndGame());
        }
    }

    private float RoundToHalf(float n)
    {
        return Mathf.Round(n * 2 + 0.01f) / 2;
    }
}