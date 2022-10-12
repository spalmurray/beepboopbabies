using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BabyPickUpInteractable))]
public class BabyState : MonoBehaviour
{
    // Public ID of the baby, to be set in unity editor
    public int id;
    public float energy = 100f;
    public float health = 100f;
    public float currentEnergy;
    public float currentHealth;
    public bool rechargeBaby;
    public BabyUIController uiController;
    // every one 1 decrement by 1
    [SerializeField] private float delayTime = 0.1f;
    [SerializeField] private float decrementAmount = 5f;
    // Can add more stuffs later, like energy, social, food, etc.
    public void Start()
    {
        currentEnergy = energy;
        currentHealth = health;
        StartCoroutine(DecreaseEnergy());
    }
    
    public void IncreaseEnergy(float target)
    {
        Debug.Log("increase energy of babies");
        currentEnergy = target;
        uiController.UpdateEnergyBar(energy, currentEnergy);
    }
    
    IEnumerator DecreaseEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            if (energy >= 0 && !rechargeBaby)
            {
                currentEnergy -= decrementAmount;
                uiController.UpdateEnergyBar(energy, currentEnergy);
            }
        }
    }
    
}
