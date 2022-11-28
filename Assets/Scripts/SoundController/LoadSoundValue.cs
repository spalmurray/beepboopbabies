using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSoundValue : MonoBehaviour
{
    public string SoundRole;//sfx    music
    void Update()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(SoundRole);
    }
}
