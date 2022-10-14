using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BabyState))]
[RequireComponent(typeof(PickUpInteractable))]
public class BabyController : MonoBehaviour
{
    // Start is called before the first frame update
    public BabyUIController uiController;
    public bool rechargeBaby
    {
        set => state.rechargeBaby = value;
    }
    // every one 1 second decrement by 2 units
    [SerializeField] private float delayTime = 1f;
    [SerializeField] private float decrementAmount = 2f;
    private BabyState state;
    private float yVelocity;
    public void Start()
    {
        state = GetComponent<BabyState>();
        StartCoroutine(DecreaseEnergy());
    }
    
    public void IncreaseEnergy(float rechargeTime)
    {
        state.currentEnergy = Mathf.SmoothDamp(state.currentEnergy, state.energy, ref yVelocity, rechargeTime);
        uiController.UpdateEnergyBar(state.energy, state.currentEnergy);
    }
    
    
    IEnumerator DecreaseEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            if (state.energy >= 0 && !state.rechargeBaby)
            {
                state.currentEnergy -= decrementAmount;
                uiController.UpdateEnergyBar(state.energy, state.currentEnergy);
            }
        }
    }
}
