using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BodyPartInteractable : KickableInteractable
{
    public enum BodyPartType
    {
        Head,
        LeftArm,
        RightArm,
        LeftLeg,
        RightLeg
    }
    public BodyPartType bodyPartType;
    public GameObject nameTextObject;
    public Renderer renderer;
    public string bodyPartName;
    private Camera _cam;
    [SerializeField] private float height = 3f;
    [SerializeField] private TMP_Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        _cam = Camera.main;
    }

    public void SetNameActive(bool active)
    {
        nameTextObject.SetActive(active);
    }
    
    public void SetBodyPartName(string bodyPart)
    {
        bodyPartName = bodyPart;
        string bodyPartText = ""; 
        switch(bodyPartType) {
            case BodyPartType.Head:
                bodyPartText = "head";
                break;
            case BodyPartType.LeftArm:
                bodyPartText = "arm";
                break;
            case BodyPartType.RightArm:
                bodyPartText = "arm";
                break;
            case BodyPartType.LeftLeg:
                bodyPartText = "leg";
                break;
            case BodyPartType.RightLeg:
                bodyPartText = "leg";
                break;
            default:
                break;
        }
        nameText.SetText(bodyPartName + "'s " + bodyPartText);
    }

    private void Update()
    {
        // make the UI face towards the camera
        var trans = nameTextObject.transform;
        trans.position = transform.position + height * Vector3.up;
        trans.rotation = Quaternion.LookRotation(trans.position - _cam.transform.position);
        var eulerAngles = trans.eulerAngles;
        eulerAngles.y = 0;
        trans.eulerAngles = eulerAngles;
    }
}
