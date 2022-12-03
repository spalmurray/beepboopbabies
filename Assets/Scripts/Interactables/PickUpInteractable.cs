using UnityEngine;

public class PickUpInteractable : Interactable
{
    public delegate void PickedUpEvent();
    public event PickedUpEvent HandlePickedUp;
    public bool isPickedUp;
    public bool inStation;
    protected Rigidbody rb;
    public void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    public override void Interact(GameObject other)
    {
        var state = other.GetComponent<AgentState>();
        if (state == null) return;

        if (!isPickedUp)
            PickUp(state);
        else
            Drop(state);
    }

    public void PickUp(AgentState state)
    {
        if (state.pickedUpObject)
        {
            // Swap picked up object with current
            state.pickedUpObject.Drop(state);
        }

        var pickUpTransform = state.pickUpPoint.transform;
        transform.parent = pickUpTransform;
        transform.localPosition = Vector3.zero;
        transform.rotation = pickUpTransform.rotation;
        state.pickedUpObject = this;
        rb.isKinematic = true;
        rb.detectCollisions = false;
        // clear the interactable slot so the agent can interact with other objects
        state.interactable = null;
        // set the agent to non interactable so Triggers won't collide with it
        gameObject.layer = LayerMask.NameToLayer("NonInteractable");
        outline.OutlineMode = Outline.Mode.OutlineHidden;
        isPickedUp = true;
        HandlePickedUp?.Invoke();
    }

    public void Drop(AgentState state)
    {
        if (isPickedUp && transform.CompareTag("Bottle"))//transform.CompareTag("Bottle")
            CharacterUpdate.Instance.PlayKickingBottle();
        if (isPickedUp && transform.parent.CompareTag("ParentPick"))
            CharacterUpdate.Instance.PlayParentsComing();
        state.pickedUpObject = null;
        rb.isKinematic = false;
        rb.detectCollisions = true;
        transform.parent = null;
        // reset to default so object can be interactable again
        gameObject.layer = LayerMask.NameToLayer("Default");
        isPickedUp = false;
    }
}