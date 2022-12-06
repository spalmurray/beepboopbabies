using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class InteractableTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public AgentState state;

    // Priority of which interactables should be prioritized. Lower is better
    private enum InteractablePriority
    {
        Baby,
        BodyPart,
        Parent,
        Station,
        Bottle,
        None
    }

    private InteractablePriority GetInteractablePriority(Interactable interactable)
    {
        switch (interactable)
        {
            case BabyPickUpInteractable:
                return InteractablePriority.Baby;
            case BodyPartInteractable:
                return InteractablePriority.BodyPart;
            case ParentInteractable:
                return InteractablePriority.Parent;
            case StationInteractable:
            case BottleStationInteractable:
                return InteractablePriority.Station;
            case BottleInteractable:
                return InteractablePriority.Bottle;
            default:
                return InteractablePriority.None;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("NonInteractable"))) return;
        var otherObject = other.gameObject;
        var interactableObj = otherObject.GetComponent<Interactable>();
        // don't interact with stations if we do not have a baby and station is empty
        if (interactableObj is StationInteractable interactable && state.pickedUpObject is not BabyPickUpInteractable
                                                                && interactable.Baby == null) return;

        var currentPriority = GetInteractablePriority(state.interactable);
        var newPriority = GetInteractablePriority(interactableObj);

        if (ReferenceEquals(state.interactable, state.pickedUpObject))
        {
            // Give the new interactable priority over picked up object
            currentPriority = InteractablePriority.None;
        }
        
        // Check that the new interactable has a higher priority (i.e. lower value)
        if (currentPriority <= newPriority) return;
        
        if (state.interactable)
        {
            HandleInteractableLeave(state.interactable.gameObject);
        }
        
        state.interactable = interactableObj;
        HandleInteractableEnter(otherObject);
    }

    private void OnTriggerExit(Collider other)
    {
        var otherObject = other.gameObject;
        var interactableObj = otherObject.GetComponent<Interactable>();
        if (state.interactable != null && ReferenceEquals(interactableObj, state.interactable))
        {
            state.interactable = null;
            HandleInteractableLeave(otherObject);
        }
    }

    private void HandleInteractableEnter(GameObject obj)
    {
        var outline = obj.GetComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        // NOTE: this only applies to bottle interactables
        var ui = obj.GetComponentInChildren<BabyUIController>();
        if (ui != null) ui.EnableStatusBars();
        var parts = obj.GetComponent<BodyPartInteractable>();
        if (parts != null) parts.SetNameActive(true);
    }

    private void HandleInteractableLeave(GameObject obj)
    {
        var ui = obj.GetComponentInChildren<BabyUIController>();
        if (ui != null) ui.DisableStatusBars();
        var parts = obj.GetComponent<BodyPartInteractable>();
        if (parts != null) parts.SetNameActive(false);
        var outline = obj.GetComponent<Outline>();
        if (outline != null) outline.OutlineMode = Outline.Mode.OutlineHidden;
    }
}