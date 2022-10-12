using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{

    public AudioSource audioSource;
    public GameObject Baby;

    public GameObject signObject;

    private bool activated=false;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("baby is near");

        if (other.tag == "Obstacle") {

            signObject.SetActive(true);

            if (!activated)
            {
                activated = true;

                audioSource.enabled=true;
                Baby.SetActive(true);
            }
        }

    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit Event");

        if (other.tag == "Obstacle")
        {

            signObject.SetActive(false);
            
        }


    }
}
