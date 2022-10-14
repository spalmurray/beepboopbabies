using UnityEngine;

public class BabyState : AgentState
{
    public string babyName;
    public float energy = 100f;
    public float health = 100f;
    public float currentEnergy;
    public float currentHealth;
    [HideInInspector] public bool rechargeBaby;

    private void Start()
    {
        currentEnergy = energy;
        currentHealth = health;
    }
}