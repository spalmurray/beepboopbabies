using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterData : MonoBehaviour
{
    public Transform[] players;
    public GameObject[] playerSelectUIs;
    public TextMeshProUGUI gameStartingText;

    public const int MaxIndex = 4;

    public static List<int> devices = new List<int>();
    public static Dictionary<int, InputDevice[]> deviceToInputDevices = new Dictionary<int, InputDevice[]>();
    // Map device id to its selected index
    public static Dictionary<int, int> deviceToIndex = new Dictionary<int, int>();

    private List<bool> lockedIn;
    
    // Can start the game when there is at least one player and all players are locked in
    public bool CanStartGame => lockedIn.Count > 0 && lockedIn.All(x => x);

    private void Start()
    {
        devices = new List<int>();
        lockedIn = new List<bool>();

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
            playerSelectUIs[i].SetActive(!lockedIn[i]);
        }

        gameStartingText.SetText(CanStartGame ? "Starting game..." : "");
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RegisterDevice(int deviceId, PlayerInput playerInput)
    {
        if (devices.Contains(deviceId) || devices.Count >= 4) return;
        
        devices.Add(deviceId);
        lockedIn.Add(false);
        if (!deviceToIndex.ContainsKey(deviceId))
        {
            deviceToIndex[deviceId] = 0;
        }

        deviceToInputDevices[deviceId] = playerInput.devices.ToArray();
        
        UpdateCharacters();
    }

    public void ToggleLockInDevice(int deviceId)
    {
        var index = devices.IndexOf(deviceId);
        if (index == -1) return;

        // Toggle locked in status
        lockedIn[index] = !lockedIn[index];
        UpdateCharacters();
        
        if (CanStartGame)
        {
            StartCoroutine(StartGameAfterDelay());
        }
    }
    
    // Prevent lock in -> unlock -> lock again, and game immediately starts
    private int startGameId = 0;

    private IEnumerator StartGameAfterDelay()
    {
        var id = ++startGameId;
        yield return new WaitForSeconds(3);
        if (CanStartGame && startGameId == id)
        {
            StartGame();
        }
    }

    public void ModifyIndex(int deviceId, int delta)
    {
        var index = devices.IndexOf(deviceId);
        if (index == -1 || lockedIn[index]) return;
        
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
