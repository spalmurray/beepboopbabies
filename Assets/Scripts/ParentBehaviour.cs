using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentBehaviour : MonoBehaviour
{
    // Parameters for each parent
    public int babyId;

    // Parent constants

    // Wait time between each action
    public float regularWaitTime = 1;

    public float moveSpeed = 5.0f;

    public GameObject babyPrefab;
    public float babySpawnOffset = 1;

    private ParentBabyPickUpTrigger babyTrigger;
    private bool isBabyPickedUp = false;

    private void Start()
    {
        babyTrigger = GetComponentInChildren<ParentBabyPickUpTrigger>();
        babyTrigger.babyId = babyId;
        babyTrigger.HandleBabyPickedUp += HandleBabyPickedUp;
        babyTrigger.gameObject.SetActive(false);
    }

    public IEnumerator SpawnBabyRoutine(Vector3[] enterPath, Vector3[] leavePath)
    {
        // Arrive, spawn baby, and leave
        yield return MovementAlongPath.Move(gameObject, enterPath, moveSpeed);
        yield return new WaitForSeconds(regularWaitTime);
        SpawnBaby();
        yield return new WaitForSeconds(regularWaitTime);
        yield return MovementAlongPath.Move(gameObject, leavePath, moveSpeed);
    }

    public IEnumerator PickUpBabyRoutine(Vector3[] enterPath, Vector3[] leavePath)
    {
        yield return MovementAlongPath.Move(gameObject, enterPath, moveSpeed);
        yield return new WaitForSeconds(regularWaitTime);

        yield return WaitForBaby();
        ScoreManager.Instance.UpdateScore(1);

        yield return new WaitForSeconds(regularWaitTime);
        yield return MovementAlongPath.Move(gameObject, leavePath, moveSpeed);
    }

    private IEnumerator WaitForBaby()
    {
        isBabyPickedUp = false;
        babyTrigger.gameObject.SetActive(true);
        yield return new WaitUntil(() => isBabyPickedUp);
        babyTrigger.gameObject.SetActive(false);
    }

    private void HandleBabyPickedUp(BabyState baby)
    {
        isBabyPickedUp = true;
        baby.gameObject.transform.parent = transform;
    }

    private void SpawnBaby()
    {
        var baby = Instantiate(
            babyPrefab,
            transform.position + transform.forward * babySpawnOffset,
            Quaternion.identity
        );
        var component = baby.GetComponent<BabyState>();
        component.id = babyId;
    }
}
