using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOrPause : MonoBehaviour
{
    public AudioSource  PauseMenu;
    public AudioSource countdown;
    public AudioListener audioListener  ;
    private void OnEnable()
    {
        audioListener.enabled = !audioListener.enabled;
        PauseMenu.Pause();
        countdown.Pause();
    }
    private void OnDisable()
    {
        audioListener.enabled = !audioListener.enabled;
        PauseMenu.Play();
        //countdown.Play();
    }
}
