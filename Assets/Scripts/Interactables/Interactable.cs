using System;
using UnityEngine;


[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    protected Outline outline;
    public abstract void Interact(GameObject playerObject);

    public void Start()
    {
        outline = gameObject.GetComponent<Outline>();
    }
}