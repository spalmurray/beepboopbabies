using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsManager : MonoBehaviour
{
    public GameObject parentPrefab;
    public PathProvider path1;
    public PathProvider path2;

    // Time before parent returns for their children
    public float returnWaitTime = 5;

    private int currentBabyId = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ManagerRoutine());
    }

    IEnumerator ManagerRoutine()
    {
        StartCoroutine(ParentRoutine(currentBabyId++, path1));
        yield return new WaitForSeconds(1);
        StartCoroutine(ParentRoutine(currentBabyId++, path2));
    }

    IEnumerator ParentRoutine(int babyId, PathProvider pathProvider)
    {
        var parent = CreateParent(babyId);

        yield return parent.SpawnBabyRoutine(pathProvider.GetPath(), pathProvider.GetReversePath());

        // Wait for returnWaitTime, then return to pick up baby
        parent.gameObject.SetActive(false);
        yield return new WaitForSeconds(returnWaitTime);
        parent.gameObject.SetActive(true);

        yield return parent.PickUpBabyRoutine(pathProvider.GetPath(), pathProvider.GetReversePath());

        Destroy(parent.gameObject);
    }

    ParentBehaviour CreateParent(int babyId)
    {
        var obj = Instantiate(parentPrefab);
        var parentBehaviour = obj.GetComponent<ParentBehaviour>();
        parentBehaviour.babyId = babyId;
        return parentBehaviour;
    }
}
