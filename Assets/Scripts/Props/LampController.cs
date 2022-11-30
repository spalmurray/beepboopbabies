using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    public Light lampLight;
    public float enableTime = 45f;
    
    void Start()
    {
        lampLight.enabled = false;
        lampLight.intensity = 0;
    }

    void Update()
    {
        var shouldEnable = ScoreManager.Instance.CurrentTime <= enableTime;
        lampLight.enabled = shouldEnable;
        if (shouldEnable && lampLight.intensity < 1)
        {
            lampLight.intensity += Time.deltaTime * 0.5f;
        }
    }
}
