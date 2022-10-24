using System;
using UnityEngine;

[RequireComponent(typeof(AgentState))]
public class StationInteractable : GenericStationInteractable<BabyPickUpInteractable>
{
     // Cached baby controller, used by mechanics to change baby state
     private BabyController baby;
     private BabyPickUpInteractable prevBabyInteractable;
     public BabyController Baby
     {
          get
          {
               if (!ReferenceEquals(prevBabyInteractable, pickedUpObject))
               {
                    prevBabyInteractable = pickedUpObject;
                    baby = pickedUpObject ? pickedUpObject.GetComponent<BabyController>() : null;
               }
               return baby;
          }
     }
}