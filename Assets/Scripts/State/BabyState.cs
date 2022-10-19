using UnityEngine;

public class BabyState : AgentState
{
    public string babyName;
    public float energy = 100f;
    public float health = 100f;
    public float diaper = 100f;
    public float fun = 100f;
    public float oil = 100f;
    // the values at which the players will be warned that this babies needs attention
    public float energyWarnThreshold = 25f;
    public float healthWarnThreshold = 25f;
    public float diaperWarnThreshold = 25f;
    public float funWarnThreshold = 25f;
    public float oilWarnThreshold = 25f;
    [HideInInspector] public float currentEnergy;
    [HideInInspector] public float currentDiaper;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentFun;
    [HideInInspector] public float currentOil;
    [HideInInspector] public bool inStation = false;
    [HideInInspector] public bool rechargeBaby;
    [HideInInspector] public bool rediaperBaby;
    [HideInInspector] public bool rechargeOil;
    [HideInInspector] public bool isFlying = false;
    [HideInInspector] public bool onGround = false;

    private void Start()
    {
        currentEnergy = energy;
        currentHealth = health;
        currentDiaper = diaper;
        currentFun = fun;
        currentOil = oil;
    }
}