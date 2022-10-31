using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BabyState))]
[RequireComponent(typeof(BabyPickUpInteractable))]
public class BabyController : MonoBehaviour
{
    // Start is called before the first frame update
    public BabyUIController uiController;
    private Renderer renderer;
    private BabyPickUpInteractable interactable;
    private Rigidbody rb;
    // every one 1 second decrement by 2 units
    [SerializeField] private float decrementAmountEnergy = 2f;
    [SerializeField] private float decrementAmountDiaper = 2f;
    [SerializeField] private float oilDrinkAmountOil = 2f;
    [SerializeField] private float decrementAmountOil = 2f;
    private BabyState state;
    [SerializeField] private float funDecreasePerSecondIdle = 2f;
    [SerializeField] private float funIncreasePerSecondFlying = 25f;
    [SerializeField] private float healthDecreasePerDrop = 25;

    public void Awake()
    {
        state = GetComponent<BabyState>();
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<BabyPickUpInteractable>();
    }

    public void Start()
    {
        uiController.SetName(state.name);
    }
    public void OnEnable()
    {
        interactable.HandlePickedUp += HandlePickedUp;
        interactable.HandleKicked += HandleKicked;
    }
    
    public void OnDisable()
    {
        interactable.HandlePickedUp -= HandlePickedUp;
        interactable.HandleKicked -= HandleKicked;
    }

    private void Update()
    {
        HandleFun();
        HandleOil();
        DecreaseDiaper();
        DecreaseEnergy();
        SetAlwaysActive();
    }
    

    private void SetAlwaysActive()
    {
        bool? healthAlwaysActive = null;
        bool? energyAlwaysActive = null;
        bool? diaperAlwaysActive = null;
        bool? funAlwaysActive = null;
        bool? oilAlwaysActive = null;
        if (state.currentHealth < state.healthWarnThreshold)
        {
            healthAlwaysActive = true;
        }
        if (state.currentEnergy < state.energyWarnThreshold)
        {
            energyAlwaysActive = true;
        }
        if (state.currentDiaper < state.diaperWarnThreshold)
        {
            diaperAlwaysActive = true;
        }
        if (state.currentFun < state.funWarnThreshold)
        {
            funAlwaysActive = true;
        }
        if (state.currentOil < state.oilWarnThreshold)
        {
            oilAlwaysActive = true;
        }
        uiController.SetAlwaysActive(healthAlwaysActive, energyAlwaysActive, diaperAlwaysActive, funAlwaysActive, oilAlwaysActive);
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

    private void HandleFun()
    {
        if (state.isFlying)
        {
            state.currentFun = Mathf.Min(state.currentFun + funIncreasePerSecondFlying * Time.deltaTime, state.fun);
            uiController.SetAlwaysActive(fun:true);
        }
        else
        {
            state.currentFun = Mathf.Max(state.currentFun - funDecreasePerSecondIdle * Time.deltaTime, 0);
            uiController.SetAlwaysActive(fun:false);
        }
        uiController.UpdateFunBar(state.fun, state.currentFun);
    }

    private void HandleOil()
    {
        if (state.pickedUpObject is BottleInteractable drinkBottle && state.currentOil <= state.oil)
        {
            drinkBottle.DecreaseAmount(oilDrinkAmountOil * Time.deltaTime);
            // if bottle is empty, drop it
            if (drinkBottle.currentAmount <= 0)
            {
                drinkBottle.Drop(state);
                return;
            }
            state.currentOil = Mathf.Min(state.currentOil + oilDrinkAmountOil * Time.deltaTime, state.oil);
            uiController.UpdateOilBar(state.oil, state.currentOil);
        }
        // if we are fill to the max drop the bottle
        else if (state.pickedUpObject is BottleInteractable dropBottle && state.currentOil >= state.oil)
        {
            dropBottle.Drop(state);
        } 
        else 
        {
            state.currentOil = Mathf.Max(state.currentOil - decrementAmountOil * Time.deltaTime, 0);
            uiController.UpdateOilBar(state.oil, state.currentOil);
        }
    }

    private void DecreaseEnergy()
    {
        if (state.currentEnergy >= 0)
        {
            state.currentEnergy = Mathf.Max(state.currentEnergy - decrementAmountEnergy * Time.deltaTime, 0);
            uiController.UpdateEnergyBar(state.energy, state.currentEnergy);
        }
    }

    private void DecreaseDiaper()
    {
        if (state.currentDiaper >= 0)
        {
            state.currentDiaper = Mathf.Max(state.currentDiaper - decrementAmountDiaper * Time.deltaTime, 0);
            uiController.UpdateDiaperBar(state.diaper, state.currentDiaper);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Room" && state.isFlying)
        {
            state.isFlying = false;
            state.currentHealth = Mathf.Max(state.currentHealth - healthDecreasePerDrop, 0);
            uiController.UpdateHealthBar(state.health, state.currentHealth);
            StartCoroutine(TemporaryAlert(health: true));
        }
    }

    private IEnumerator TemporaryAlert(bool? health = null, bool? energy = null, bool? diaper = null, bool? fun = null, bool? oil = null)
    {
        uiController.SetAlwaysActive(health, energy, diaper, fun, oil);
        yield return new WaitForSeconds(1f);
        if (health.GetValueOrDefault(false)) uiController.SetAlwaysActive(health: false);
        if (energy.GetValueOrDefault(false)) uiController.SetAlwaysActive(energy: false);
        if (diaper.GetValueOrDefault(false)) uiController.SetAlwaysActive(diaper: false);
        if (fun.GetValueOrDefault(false)) uiController.SetAlwaysActive(fun: false);
        if (oil.GetValueOrDefault(false)) uiController.SetAlwaysActive(oil: false);
    }

    private void HandlePickedUp()
    {
        state.isFlying = false;
    }

    private void HandleKicked()
    {
        state.isFlying = true;
    }
}