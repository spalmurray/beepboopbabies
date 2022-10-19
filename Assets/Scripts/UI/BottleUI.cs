using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleUI : MonoBehaviour
{
    [SerializeField] private Image fillSprite;
    [SerializeField] private GameObject fillbar;
    [SerializeField] private Transform bottleLocation;
    [SerializeField] private float height = 3f;
    private Camera _cam;
    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;
    }
    private void Update()
    {
        // make the UI face towards the camera
        var trans = transform;
        trans.position = bottleLocation.position +  height * Vector3.up;
        trans.rotation = Quaternion.LookRotation(trans.position - _cam.transform.position);
        var eulerAngles = trans.eulerAngles;
        eulerAngles.y = 0;
        trans.eulerAngles = eulerAngles;
    }

    public void UpdateBar(float max, float current)
    {
        fillSprite.fillAmount = current / max;
    }
    
    public void DisableStatusBars()
    {
        fillbar.SetActive(false);
    }
    
    public void EnableStatusBars()
    {
        fillbar.SetActive(true);
    }
    
}
