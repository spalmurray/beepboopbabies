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
    private float yVelocity;

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

    public void IncreaseEnergy(float rechargeTime)
    {
        state.currentEnergy = Mathf.SmoothDamp(state.currentEnergy, state.energy, ref yVelocity, rechargeTime);
        uiController.UpdateEnergyBar(state.energy, state.currentEnergy);
    }


    private IEnumerator DecreaseEnergy()
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


    public void IncreaseDiaper(float rediaperTime)
    {
        state.currentDiaper = Mathf.SmoothDamp(state.currentDiaper, state.diaper, ref yVelocity, rediaperTime);
        uiController.UpdateDiaperBar(state.diaper, state.currentDiaper);
    }

    private IEnumerator DecreaseDiaper()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTimediaper);
            if (state.diaper >= 0 && !state.rediaperBaby)
            {
                state.currentDiaper -= decrementAmountdiaper;
                uiController.UpdateDiaperBar(state.diaper, state.currentDiaper);
            }
        }
    }

}