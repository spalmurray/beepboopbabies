using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(StarsUIController))]
public class StarsPanel : MonoBehaviour
{
    public static StarsPanel Instance;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void ShowStars(float stars)
    {
        GetComponent<StarsUIController>().ShowStars(stars);
    }
}
