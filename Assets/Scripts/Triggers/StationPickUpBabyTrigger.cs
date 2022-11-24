using UnityEngine;

[RequireComponent(typeof(StationInteractable))]
public class StationPickUpBabyTrigger : MonoBehaviour
{
    private StationInteractable station;
    
    void Start()
    {
        station = GetComponent<StationInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (station.pickedUpObject) return;
        var babyInteractable = other.gameObject.GetComponent<BabyPickUpInteractable>();
        var state = other.gameObject.GetComponent<BabyState>();
        if (babyInteractable && !babyInteractable.isPickedUp && state && state.isFlying)
        {
            station.PickUpObject(babyInteractable);
        }
    }
}
