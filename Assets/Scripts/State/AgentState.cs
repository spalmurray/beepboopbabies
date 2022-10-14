using UnityEngine;

/// <summary>
///     Container which Holds the State of every Agent Object
///     Agents represent anything that can pick up objects
/// </summary>
public class AgentState : MonoBehaviour
{
    // a point which the pick up object will be snapped to
    public Transform pickUpPoint;

    // objects we have picked up
    public PickUpInteractable pickedUpObject;

    // objects we can potentially interact with
    public Interactable interactable;
}