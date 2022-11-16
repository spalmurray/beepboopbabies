using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerSelectionController : MonoBehaviour
{
    public float moveThreshold = 0.5f;
    
    public int deviceId;

    private PlayerInput playerInput;
    private CharacterData characterData;

    private bool registered = false;
    private bool moved = false;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        characterData = FindObjectOfType<CharacterData>();
        deviceId = playerInput.devices[0].deviceId;
    }

    private void OnMove(InputValue value)
    {
        if (!registered)
        {
            characterData.RegisterDevice(deviceId);
            registered = true;
        }
        
        var x = value.Get<Vector2>().x;
        if (x > moveThreshold)
        {
            if (moved) return;
            characterData.ModifyIndex(deviceId, 1);
            moved = true;
        }
        else if (x < -moveThreshold)
        {
            if (moved) return;
            characterData.ModifyIndex(deviceId, -1);
            moved = true;
        }
        else
        {
            moved = false;
        }
    }
}