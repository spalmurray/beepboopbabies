using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerObject;

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObject.transform.position;
    }
}
