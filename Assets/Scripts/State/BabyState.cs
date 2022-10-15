using UnityEngine;

public class BabyState : AgentState
{
    public string babyName;
    public float energy = 100f;
    public float health = 100f;
    public float diaper = 100f;
    public float currentEnergy;
    public float currentDiaper;
    public float currentHealth;
    [HideInInspector] public bool rechargeBaby;
    [HideInInspector] public bool rediaperBaby;

    private void Start()
    {
        currentEnergy = energy;
        currentHealth = health;
        currentDiaper = diaper;
    }
}