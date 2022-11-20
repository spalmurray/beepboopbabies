using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpdate : MonoBehaviour
{
    public static CharacterUpdate Instance;
    public Color[] colors;
    public int index = -1;
    public int CharacterNum = 0;
    public AudioClip KickingSound;
    private void Awake()
    {
        Instance = this;
    }
    public Color GetColor()
    {
        index++;
        return colors[index];
    }
    public void PlayKickingSound()
    {
        GetComponent<AudioSource>().PlayOneShot(KickingSound);
    }
}
