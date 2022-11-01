using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public AgentState state;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("NonInteractable"))) return;
        var otherObject = other.gameObject;
        var interactableObj = otherObject.GetComponent<Interactable>();
        // don't interact with stations if we do not have a baby and station is empty
        if (interactableObj is StationInteractable interactable && state.pickedUpObject == null
                                                                && interactable.Baby == null) return;
        if (state.interactable == null && interactableObj != null)
        {
            state.interactable = interactableObj;
            var outline = otherObject.GetComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            // NOTE: this only applies to bottle interactables
            var ui = otherObject.GetComponentInChildren<BabyUIController>();
            if (ui != null) ui.EnableStatusBars();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var otherObject = other.gameObject;
        var interactableObj = otherObject.GetComponent<Interactable>();
        if (state.interactable != null && ReferenceEquals(interactableObj, state.interactable))
        {
            state.interactable = null;
            var ui = otherObject.GetComponentInChildren<BabyUIController>();
            if (ui != null) ui.DisableStatusBars();
        }

        var outline = otherObject.GetComponent<Outline>();
        if (outline != null) outline.OutlineMode = Outline.Mode.OutlineHidden;
    }
}