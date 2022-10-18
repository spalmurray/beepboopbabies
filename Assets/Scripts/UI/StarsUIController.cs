using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsUIController : MonoBehaviour
{
    public Image[] starImages;

    public void ShowStars(float stars)
    {
        foreach (var starImage in starImages)
        {
            starImage.fillAmount = Mathf.Clamp(stars, 0, 1);
            stars--;
        }
    }
}
