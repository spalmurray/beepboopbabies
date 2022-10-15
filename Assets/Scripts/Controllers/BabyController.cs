using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BabyState))]
[RequireComponent(typeof(PickUpInteractable))]
public class BabyController : MonoBehaviour
{
    // Start is called before the first frame update
    public BabyUIController uiController;

    // every one 1 second decrement by 2 units
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private float decrementAmount = 2f;
    [SerializeField] private float delayTimediaper = 1f;
    [SerializeField] private float decrementAmountdiaper = 2f;
    private BabyState state;

    public bool rechargeBaby
    {
        set => state.rechargeBaby = value;
    }

    public bool rediaperBaby
    {
        set => state.rediaperBaby = value;
    }

    public void Start()
    {
        state = GetComponent<BabyState>();
        StartCoroutine(DecreaseEnergy());
        StartCoroutine(DecreaseDiaper());
    }

    public void IncreaseEnergy(float incrementAmount)
    {
        state.currentEnergy += incrementAmount;
        state.currentEnergy = Math.Clamp(state.currentEnergy, 0f, state.energy);
        uiController.UpdateEnergyBar(state.energy, state.currentEnergy);
    }
    
    public void IncreaseDiaper(float incrementAmount)
    {
        state.currentDiaper += incrementAmount;
        state.currentDiaper = Math.Clamp(state.currentDiaper, 0f, state.diaper);
        uiController.UpdateDiaperBar(state.diaper, state.currentDiaper);
    }


    private IEnumerator DecreaseEnergy()
    {
        var wait = new WaitForSeconds(delayTime);
        while (true)
        {
            yield return wait;
            if (state.energy >= 0 && !state.rechargeBaby)
            {
                state.currentEnergy -= decrementAmount;
                uiController.UpdateEnergyBar(state.energy, state.currentEnergy);
            }
        }
    }
    

    private IEnumerator DecreaseDiaper()
    {
        var wait = new WaitForSeconds(delayTimediaper);
        while (true)
        {
            yield return wait;
            if (state.diaper >= 0 && !state.rediaperBaby)
            {
                state.currentDiaper -= decrementAmountdiaper;
                uiController.UpdateDiaperBar(state.diaper, state.currentDiaper);
            }
        }
    }

}