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
        if (state.interactable == null && interactableObj != null) state.interactable = interactableObj;
    }

    private void OnTriggerExit(Collider other)
    {
        var interactableObj = other.gameObject.GetComponent<Interactable>();
        if (ReferenceEquals(interactableObj, state.interactable)) state.interactable = null;
    }
}