using System.Collections.Generic;
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
    public float currentEnergy;
    public float currentDiaper;
    public float currentHealth;
    public float currentFun;
    public float currentOil;
    public float explosionRadius = 10f;
    public float explosionForce = 50f;
    public List<GameObject> peers = new();
    public bool isSad;
    [HideInInspector] public bool rechargeBaby;
    [HideInInspector] public bool rediaperBaby;
    [HideInInspector] public bool rechargeOil;
    public bool isFlying = false;
    [HideInInspector] public bool onGround = false;
    public float GetCurrent()
    {
        return (currentEnergy + currentDiaper + currentHealth + currentFun + currentOil) / 500;
    }
    private void Start()
    {

        float randomNeeds = Random.Range(0.5f, 0.7f);
        //set random needs to 50%-70%
        int index = Random.Range(0, 5);
        //random choose one of the needs
        switch (index)
        {
            case 0:
                currentEnergy = energy * randomNeeds;
                currentHealth = health;
                currentDiaper = diaper;
                currentFun = fun;
                currentOil = oil;
                break;
            case 1:
                currentEnergy = energy;
                currentHealth = health * randomNeeds;
                currentDiaper = diaper;
                currentFun = fun;
                currentOil = oil;
                break;
            case 2:
                currentEnergy = energy;
                currentHealth = health;
                currentDiaper = diaper * randomNeeds;
                currentFun = fun;
                currentOil = oil;
                break;
            case 3:
                currentEnergy = energy;
                currentHealth = health;
                currentDiaper = diaper;
                currentFun = fun * randomNeeds;
                currentOil = oil;
                break;
            case 4:
                currentEnergy = energy;
                currentHealth = health;
                currentDiaper = diaper;
                currentFun = fun;
                currentOil = oil * randomNeeds;
                break;
            default:
                Debug.LogError("error needs");
                break;
        }
    }
    bool temp = false;
    bool mfly = false;
    private void Update()
    {
        if (currentHealth <= 0 && !temp)
        {
            temp = true;
            EZCameraShake.CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        }
        if(isFlying && mfly == false)
        {
            mfly = true;
            CharacterUpdate.Instance.PlayKickingSound();
        }
        if(!isFlying )
        {
            mfly = false;
        }
    }
}