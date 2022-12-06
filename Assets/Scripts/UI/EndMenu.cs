using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public GameObject endMenuUI;
    public Button initialButton;
    public StarsUIController starsUIController;
    public GameObject NextLevelButton;
    public Button NextLevel;
    public Button Menu;
    public Button Restart;
    public AudioSource theme;
    public AudioSource win;
    public AudioSource fail;
    [Header("")]
    [SerializeField]
    private AudioClip[] audioClips;

    void Start()
    {
        NextLevel.enabled = false;
        ScoreManager.Instance.HandleGameOver += ShowMenu;
        AudioClip audioClip = null;
        switch (audioClips.Length)
        {
            case 1:
                audioClip = audioClips[0];
                break;
            case 2:
                if (LevelsManager.Instance.Level == 0 || LevelsManager.Instance.Level == 2 ||
                    LevelsManager.Instance.Level == 4 || LevelsManager.Instance.Level == 6)
                    audioClip = audioClips[0];
                else
                    audioClip = audioClips[1];
                break;
            case 3:
                if (LevelsManager.Instance.Level == 0 || LevelsManager.Instance.Level == 3
                    || LevelsManager.Instance.Level == 6)
                    audioClip = audioClips[0];
                else if (LevelsManager.Instance.Level == 1 || LevelsManager.Instance.Level == 4)
                    audioClip = audioClips[1];
                else
                    audioClip = audioClips[2];
                break;
            case 4:
                if (LevelsManager.Instance.Level == 0 || LevelsManager.Instance.Level == 4)
                    audioClip = audioClips[0];
                else if (LevelsManager.Instance.Level == 1 || LevelsManager.Instance.Level == 5)
                    audioClip = audioClips[1];
                else if (LevelsManager.Instance.Level == 2 || LevelsManager.Instance.Level == 6)
                    audioClip = audioClips[2];
                else
                    audioClip = audioClips[3];
                break;
            case 5:
                if (LevelsManager.Instance.Level == 0 || LevelsManager.Instance.Level == 5)
                    audioClip = audioClips[0];
                else if (LevelsManager.Instance.Level == 1 || LevelsManager.Instance.Level == 6)
                    audioClip = audioClips[1];
                else if (LevelsManager.Instance.Level == 2)
                    audioClip = audioClips[2];
                else if (LevelsManager.Instance.Level == 3)
                    audioClip = audioClips[3];
                else
                    audioClip = audioClips[4];
                break;
            case 6:
                if (LevelsManager.Instance.Level == 0 || LevelsManager.Instance.Level == 6)
                    audioClip = audioClips[0];
                else if (LevelsManager.Instance.Level == 1)
                    audioClip = audioClips[1];
                else if (LevelsManager.Instance.Level == 2)
                    audioClip = audioClips[2];
                else if (LevelsManager.Instance.Level == 3)
                    audioClip = audioClips[3];
                else if (LevelsManager.Instance.Level == 4)
                    audioClip = audioClips[4];
                else
                    audioClip = audioClips[5];
                break;
            case 7:
                audioClip = audioClips[LevelsManager.Instance.Level];
                break;
            default:
                break;
        }
        theme.clip = audioClip;
        theme.Play(); //LevelsManager.Instance.NextLevel();
        GetComponent<AudioSource>().Stop();

    }

    void ShowMenu()
    {
        if (ScoreManager.Instance.FinalScore >= 3.0)
        {
            endMenuUI.SetActive(true);
            initialButton.Select();
            starsUIController.ShowStars(ScoreManager.Instance.FinalScore);
            theme.Stop();
            win.Stop();
            win.loop = true;
            win.Play();

        }
        else
        {
            endMenuUI.SetActive(true);
            initialButton.Select();
            starsUIController.ShowStars(ScoreManager.Instance.FinalScore);
            NextLevelButton.SetActive(false);
            theme.Stop();
            fail.Stop();
            fail.loop = true;
            fail.Play();
        }

    }

    void Update()
    {
        if (ScoreManager.Instance.FinalScore >= 3.0)
        {
            NextLevel.enabled = true;

        }
        else
        {
            NextLevel.enabled = false;
        }
    }
}
