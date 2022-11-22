using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform[] spawnLocations;

    private void Start()
    {
        var index = 0;
        foreach (var deviceId in CharacterData.devices)
        {
            // The player input in character data should already be destroyed by unity
            // So we cannot use it directly. Instead just pull the device data from it
            var playerInput = CharacterData.deviceToPlayerInput[deviceId];
            PlayerInputManager.instance.JoinPlayer(index++, -1, playerInput.currentControlScheme, playerInput.devices.ToArray());
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        var location = spawnLocations[playerInput.playerIndex];
        playerInput.gameObject.GetComponent<PlayerController>().startPosition = location.position;
    }
}