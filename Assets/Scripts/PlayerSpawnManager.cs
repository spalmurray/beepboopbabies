using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform[] spawnLocations;

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        var location = spawnLocations[playerInput.playerIndex];
        playerInput.gameObject.GetComponent<PlayerMovement>().startPosition = location.position;
    }
}
