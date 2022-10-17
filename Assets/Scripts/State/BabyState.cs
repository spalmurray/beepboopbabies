using UnityEngine;

public class BabyState : AgentState
{
    public string babyName;
    public float energy = 100f;
    public float health = 100f;
    public float diaper = 100f;
    public float fun = 100f;
    public float currentEnergy;
    public float currentDiaper;
    public float currentHealth;
    public float currentFun;
    [HideInInspector] public bool rechargeBaby;
    [HideInInspector] public bool rediaperBaby;
    [HideInInspector] public bool isFlying = false;

    private void Start()
    {
        currentEnergy = energy;
        currentHealth = health;
        currentDiaper = diaper;
        currentFun = fun;
    }
}