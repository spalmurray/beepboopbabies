using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyUIController : MonoBehaviour
{
    
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private Image energybarSprite;
    [SerializeField] private GameObject healthbar;
    [SerializeField] private GameObject energybar;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Image dialogImage;
    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthbarSprite.fillAmount = currentHealth / maxHealth;
    }
    public void UpdateEnergyBar(float maxEnergy, float currentEnergy)
    {
        energybarSprite.fillAmount = currentEnergy / maxEnergy;
    }

    public void EnableStatusBars()
    {
        healthbar.SetActive(true);
        energybar.SetActive(true);
    }
    
    public void DisableStatusBars()
    {
        healthbar.SetActive(true);
        energybar.SetActive(true);
    }
    public void EnableDialogBox(Sprite sprite)
    {
        dialogBox.SetActive(true);
        dialogImage.sprite = sprite;
    }
    public void DisableDialogBox()
    {
        dialogBox.SetActive(false);
        dialogImage.sprite = null;
    }
}
