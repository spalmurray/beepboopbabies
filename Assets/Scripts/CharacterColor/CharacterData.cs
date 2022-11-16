using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterData : MonoBehaviour
{
    public Transform[] players;

    public const int MaxIndex = 4;

    // Map device id to its selected index
    public static Dictionary<int, int> deviceToIndex = new Dictionary<int, int>();

    private List<int> devices;

    private void Start()
    {
        devices = new List<int>();

        UpdateCharacters();
    }

    private void UpdateCharacters()
    {
        for (int i = 0; i < 4; i++)
        {
            foreach (var player in players)
            {
                player.GetChild(i).gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (devices.Count <= i) break;
            players[i].GetChild(deviceToIndex[devices[i]]).gameObject.SetActive(true);
        }
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RegisterDevice(int deviceId)
    {
        if (devices.Contains(deviceId) || devices.Count >= 4) return;
        
        devices.Add(deviceId);
        if (!deviceToIndex.ContainsKey(deviceId))
        {
            deviceToIndex[deviceId] = 0;
        }
        
        UpdateCharacters();
    }

    public void ModifyIndex(int deviceId, int delta)
    {
        deviceToIndex[deviceId] += delta;
        deviceToIndex[deviceId] %= MaxIndex;
        if (deviceToIndex[deviceId] < 0)
        {
            deviceToIndex[deviceId] += MaxIndex;
        }
        
        UpdateCharacters();
    }

    public void Btn1(bool IsLeft)
    {
        if (devices.Count < 1) return;
        ModifyIndex(devices[0], IsLeft ? -1 : 1);
    }
    public void Btn2(bool IsLeft)
    {
        if (devices.Count < 2) return;
        ModifyIndex(devices[1], IsLeft ? -1 : 1);
    }
    public void Btn3(bool IsLeft)
    {
        if (devices.Count < 3) return;
        ModifyIndex(devices[2], IsLeft ? -1 : 1);
    }
    public void Btn4(bool IsLeft)
    {
        if (devices.Count < 4) return;
        ModifyIndex(devices[3], IsLeft ? -1 : 1);
    }
}
