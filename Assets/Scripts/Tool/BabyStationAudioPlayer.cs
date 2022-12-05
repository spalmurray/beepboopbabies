using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(StationInteractable))]
public class BabyStationAudioPlayer : MonoBehaviour
{
    public Predicate<BabyState> shouldStartAudio;
    public Predicate<BabyState> shouldEndAudio;
    
    public AudioClip finishAudio;
    private AudioSource audioSource;

    private BabyState currentBaby;
    private bool isCurrentBabyFinished = true;
    private bool isPaused = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HandleBabyPlaced(bool placed)
    {
        if (placed)
        {
            currentBaby = GetComponent<StationInteractable>().Baby.GetComponent<BabyState>();
            if (shouldStartAudio(currentBaby))
            {
                isCurrentBabyFinished = false;
                audioSource.Play();
            }
        }
        else
        {
            currentBaby = null;
            audioSource.Stop();
            isCurrentBabyFinished = true;
        }
    }

    private void Update()
    {
        if (Time.deltaTime == 0 && audioSource.isPlaying)
        {
            isPaused = true;
            audioSource.Pause();
        }
        else if (Time.timeScale != 0 && isPaused)
        {
            isPaused = false;
            audioSource.UnPause();
        }
        
        if (currentBaby && !isCurrentBabyFinished && shouldEndAudio(currentBaby))
        {
            audioSource.Stop();
            if (finishAudio)
            {
                audioSource.PlayOneShot(finishAudio);
            }

            isCurrentBabyFinished = true;
        }
    }
}
