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
    
    void Start()
    {
        NextLevel.enabled = false;
        ScoreManager.Instance.HandleGameOver += ShowMenu;
        theme.Play();
		GetComponent<AudioSource>().Stop();
        
    }

    void ShowMenu()
    {
        if (ScoreManager.Instance.FinalScore >= 3.0 || LevelsManager.Instance.IsTutorial) { 
            endMenuUI.SetActive(true);
            initialButton.Select();
            starsUIController.ShowStars(ScoreManager.Instance.FinalScore);
            theme.Stop();
			win.Stop();
			win.loop = true;
			win.Play();

        } else {
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

    void Update() {
        if (ScoreManager.Instance.FinalScore >= 3.0 || LevelsManager.Instance.IsTutorial) {
            NextLevel.enabled = true;
            
        } else {
            NextLevel.enabled = false;
        }
    }
}
