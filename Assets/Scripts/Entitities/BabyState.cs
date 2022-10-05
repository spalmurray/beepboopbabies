using System;
using System.Collections;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BabyPickUpInteractable))]
public class BabyState : MonoBehaviour
{
    // Public ID of the baby, to be set in unity editor
    public int id;
    // Can add more stuffs later, like energy, social, food, etc.
    public Vector3 targetLocation = Vector3.zero;
    public float energy = 100f;
    public float health = 100f;
    public float currentEnergy;
    public float currentHealth;
    public bool rechargeBaby;
    public BabyUIController uiController;
    // every one 1 decrement by 1
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private float decrementAmount = 1f;
    private BehaviourTreeRunner runner;
    private NavMeshAgent agent;
    public void Start()
    {
        currentEnergy = energy;
        currentHealth = health;
        runner = GetComponent<BehaviourTreeRunner>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(DecreaseEnergy());
    }
    public void DisableAI()
    {
        runner.enabled = false;
        agent.enabled = false;
    }

    public void EnableAI()
    {
        runner.enabled = true;
        agent.enabled = true;
    }
    
    public void IncreaseEnergy(float target)
    {
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
