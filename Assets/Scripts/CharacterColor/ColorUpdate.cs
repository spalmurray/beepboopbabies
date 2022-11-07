using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorUpdate : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Image>().color = CharacterUpdate.Instance.GetColor();
    }
}
