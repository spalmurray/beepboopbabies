using UnityEngine;
using UnityEngine.UI;

public class ScreenMinMax : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged);
    }
    private void OnValueChanged(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("maxmize");
            if (!Screen.fullScreen)
            {
                //get current screen size
                Resolution[] resolutions = Screen.resolutions;
                //set current screen size
                Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
                Screen.fullScreen = true;  //set full screen
            }
        }
        else
        {
            Debug.Log("minmize");
            if (Screen.fullScreen)
            {
                Resolution[] resolutions = Screen.resolutions;
                Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, false);
                Screen.fullScreen = false;  

            }
        }
    }
}
