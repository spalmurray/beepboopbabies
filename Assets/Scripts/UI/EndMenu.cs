using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public GameObject endMenuUI;
    public Button initialButton;
    public StarsUIController starsUIController;
    
    void Start()
    {
        ScoreManager.Instance.HandleGameOver += ShowMenu;
    }

    void ShowMenu()
    {
        endMenuUI.SetActive(true);
        initialButton.Select();
        starsUIController.ShowStars(ScoreManager.Instance.FinalScore);
    }
}