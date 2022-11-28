using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSoundValue : MonoBehaviour
{
    public string SoundRole;
    void Update()
    {
        PlayerPrefs.SetFloat(SoundRole, GetComponent<Slider>().value);
    }
}
