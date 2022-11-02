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
    
    void Start()
    {
        NextLevel.enabled = false;
        ScoreManager.Instance.HandleGameOver += ShowMenu;
    }

    void ShowMenu()
    {
        if (ScoreManager.Instance.FinalScore >= 2.5) { 
            endMenuUI.SetActive(true);
            initialButton.Select();
            starsUIController.ShowStars(ScoreManager.Instance.FinalScore);
        } else {
            endMenuUI.SetActive(true);
            initialButton.Select();
            starsUIController.ShowStars(ScoreManager.Instance.FinalScore);
            NextLevelButton.SetActive(false);
        }
        
    }

    void Update() {
        if (ScoreManager.Instance.FinalScore >= 2.5) {
            NextLevel.enabled = true;
        } else {
            NextLevel.enabled = false;
        }
    }
}
