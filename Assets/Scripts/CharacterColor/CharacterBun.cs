using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBun : MonoBehaviour
{
    public PlayerInput playerInput;

    private void OnEnable()
    {
        var index = playerInput.devices
            .Select(device => device.deviceId)
            .Where(deviceId => CharacterData.deviceToIndex.ContainsKey(deviceId))
            .Select(deviceId => CharacterData.deviceToIndex[deviceId])
            .FirstOrDefault();

        Show(index);
    }
    
    private void Show(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        var activePlayer = transform.GetChild(index).gameObject;
        activePlayer.SetActive(true);
        GetComponentInParent<PlayerController>().anim = activePlayer.GetComponent<Animator>();
    }
}
