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
            var devices = CharacterData.deviceToInputDevices[deviceId];
            PlayerInputManager.instance.JoinPlayer(index++, -1, null, devices);
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        var location = spawnLocations[playerInput.playerIndex];
        playerInput.gameObject.GetComponent<PlayerController>().startPosition = location.position;
    }
}