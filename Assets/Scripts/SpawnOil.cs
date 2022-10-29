using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOil : MonoBehaviour
{
    public GameObject Oil;
    bool tempInvoke = true;
    private Vector3 TempPos = new Vector3(8.24f, -0.39f, 8.95f);
    void Update()
    {
        if (GetComponent<Outline>().OutlineMode.Equals(Outline.Mode.OutlineAll))
        {
            Debug.Log("spwan bottle");
            if (Input.GetKeyDown(KeyCode.E) && tempInvoke)
            {
                tempInvoke = false;
                Instantiate(Oil, TempPos, Quaternion.identity);
                Invoke("CanSpawn", 1.5f);//1.5s per spawn
            }
        }
    }
    private void CanSpawn()
    {
        tempInvoke = true;
    }
}
