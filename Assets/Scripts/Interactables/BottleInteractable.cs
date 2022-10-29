using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleInteractable : PickUpInteractable
{
    public float maxAmount = 100f;
    public float currentAmount = 0f;
    public BottleUI uiController;

    public void IncreaseAmount(float incrementAmount)
    {
        currentAmount += incrementAmount;
        currentAmount = Math.Clamp(currentAmount, 0f, maxAmount);
        uiController.UpdateBar(maxAmount, currentAmount);
    }
    
    public void DecreaseAmount(float decrementAmount)
    {
        currentAmount -= decrementAmount;
        currentAmount = Math.Clamp(currentAmount, 0f, maxAmount);
        uiController.UpdateBar(maxAmount, currentAmount);

        if (currentAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
