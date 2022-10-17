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
    [SerializeField] private float delayTimeEnergy = 1f;
    [SerializeField] private float decrementAmount = 2f;
    [SerializeField] private float delayTimeDiaper = 1f;
    [SerializeField] private float decrementAmountDiaper = 2f;
    private BabyState state;

    [SerializeField] private float funDecreasePerSecondIdle = 2f;
    [SerializeField] private float funIncreasePerSecondFlying = 25f;
    [SerializeField] private float healthDecreasePerDrop = 25;

    public bool inStation
    {
        set => state.inStation = value;
    }

    public void Start()
    {
        state = GetComponent<BabyState>();
        StartCoroutine(DecreaseEnergy());
        StartCoroutine(DecreaseDiaper());
        GetComponent<PickUpInteractable>().HandlePickedUp += HandlePickedUp;
    }

    private void Update()
    {
        if (state.isFlying)
        {
            state.currentFun = Mathf.Min(state.currentFun + funIncreasePerSecondFlying * Time.deltaTime, state.fun);
        }
        else
        {
            state.currentFun = Mathf.Max(state.currentFun - funDecreasePerSecondIdle * Time.deltaTime, 0);
        }
        uiController.UpdateFunBar(state.fun, state.currentFun);
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
    
    public void IncreaseHealth(float incrementAmount)
    {
        state.currentHealth += incrementAmount;
        state.currentHealth = Math.Clamp(state.currentHealth, 0f, state.health);
        uiController.UpdateHealthBar(state.health, state.currentHealth);
    }

    private IEnumerator DecreaseEnergy()
    {
        var wait = new WaitForSeconds(delayTimeEnergy);
        while (true)
        {
            yield return wait;
            if (state.energy >= 0 && !state.inStation)
            {
                state.currentEnergy -= decrementAmount;
                uiController.UpdateEnergyBar(state.energy, state.currentEnergy);
            }
        }
    }

    private IEnumerator DecreaseDiaper()
    {
        var wait = new WaitForSeconds(delayTimeDiaper);
        while (true)
        {
            yield return wait;
            if (state.diaper >= 0 && !state.inStation)
            {
                state.currentDiaper -= decrementAmountDiaper;
                uiController.UpdateDiaperBar(state.diaper, state.currentDiaper);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Room" && state.isFlying)
        {
            state.isFlying = false;
            state.currentHealth = Mathf.Max(state.currentHealth - healthDecreasePerDrop, 0);
            uiController.UpdateHealthBar(state.health, state.currentHealth);
        }
        else
        {
            // TODO: maybe when we throw successfully into a station, set flying to false
            state.isFlying = false;
        }
    }

    private void HandlePickedUp()
    {
        state.isFlying = false;
    }

    public void HandleKicked()
    {
        state.isFlying = true;
    }
}