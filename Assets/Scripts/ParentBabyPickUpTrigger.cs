using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentBabyPickUpTrigger : MonoBehaviour
{
    public delegate void BabyPickUpHandler(BabyState baby);

    public int babyId;
    public event BabyPickUpHandler HandleBabyPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        var baby = other.gameObject.GetComponent<BabyState>();
        if (baby != null && babyId == baby.id)
        {
            HandleBabyPickedUp(baby);
        }
    }
}
