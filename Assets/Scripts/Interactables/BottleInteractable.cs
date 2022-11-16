using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleInteractable : KickableInteractable
{
    public float maxAmount = 100f;
    public float currentAmount = 0f;

    public void IncreaseAmount(float incrementAmount)
    {
        currentAmount += incrementAmount;
        currentAmount = Math.Clamp(currentAmount, 0f, maxAmount);
    }
    
    public void DecreaseAmount(float decrementAmount)
    {
        currentAmount -= decrementAmount;
        currentAmount = Math.Clamp(currentAmount, 0f, maxAmount);

        if (currentAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
