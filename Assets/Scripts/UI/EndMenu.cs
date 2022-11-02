using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public GameObject endMenuUI;
    public Button initialButton;
    public StarsUIController starsUIController;
    public Button NextLevel;
    public Button Menu;
    
    void Start()
    {
        NextLevel.enabled = false;
        ScoreManager.Instance.HandleGameOver += ShowMenu;
    }

    void ShowMenu()
    {
        endMenuUI.SetActive(true);
        initialButton.Select();
        starsUIController.ShowStars(ScoreManager.Instance.FinalScore);
    }

    void Update() {
        if (ScoreManager.Instance.FinalScore >= 2.5) {
            NextLevel.enabled = true;
        } else {
            NextLevel.enabled = false;
        }
    }
}
