using System;
using System.Collections;
using UnityEngine;
using BBUnity.Conditions;
using TMPro;

public class TutorialSequenceController : MonoBehaviour
{
    public GameObject controlsUI;

    public GameObject dialogContainer;
    public TextMeshProUGUI dialogText;
        
    private PlayerController[] playerControllers;
    
    void Start()
    {   
        StartCoroutine(StartTutorial());
    }

    private bool healthTutorialStarted;
    private bool healthTutorialFinished;

    private void Init()
    {
        playerControllers = FindObjectsOfType<PlayerController>();
        
        FindObjectOfType<DiaperStationInteractable>().HandlePlaceEvent += isIn => inDiaperStation = isIn;
        FindObjectOfType<RechargeEnergy>()
            .GetComponent<StationInteractable>()
            .HandlePlaceEvent += isIn => inRechargeStation = isIn;
        FindObjectOfType<HealthStationInteractble>().HandlePlaceEvent += isIn => inRepairStation = isIn;

        StartCoroutine(StartHealthTutorialIfBodyPartDetached());
    }

    private IEnumerator StartTutorial()
    {
        // 1 sec just in case stuffs are still loading
        yield return new WaitForSeconds(1);
        Init();
        
        // Show controls
        SetPause(true);
        controlsUI.SetActive(true);
        yield return PollInteract();
        controlsUI.SetActive(false);
        SetPause(false);

        // Spawn parent
        yield return new WaitForSeconds(1);
        FindObjectOfType<TutorialParentSpawnManager>().SpawnParents();
        yield return new WaitForSeconds(2);
        yield return ShowDialog("A parent is here to drop off their baby! When they do, go to the baby " +
                                "and press 'Interact' to pick them up.");

        yield return WaitUntilBabyFound();
        babyState.currentHealth = babyState.health;
        babyState.currentDiaper = babyState.diaper * 0.2f;
        babyState.currentFun = babyState.fun * 0.6f;
        babyState.currentEnergy = babyState.energy * 0.6f;
        babyState.currentOil = babyState.oil * 0.8f;
        
        yield return WaitUntilBabyPickedUp();

        yield return ShowDialog(
            "A baby has five needs: health, diaper, energy, happiness, and oil. " +
            "Health is decreased when you damage the baby, and the other four needs drain over time.");
        yield return ShowDialog(
            "At the end of the day, our score is based on how well the babies' needs are fulfilled! " +
            "Don't let the need fall to zero; otherwise bad things might happen :D");
        yield return ShowDialog(
            "Look, the baby needs a diaper change! Go to the diaper station (the blue table on top) " +
            "and interact with the diaper station with the baby picked up.");

        yield return WaitUntilBabyInDiaperStation();
        yield return ShowDialog("Nice. Now, hold down the Kick/Fix button until the diaper bar is filled.");

        yield return new WaitUntil(() => babyState.currentDiaper >= 0.95f * babyState.diaper);

        yield return ShowDialog(
            "Great. Now, the baby seems to be unhappy and needs energy. Luckily, we can fill both of these at once!");
        yield return ShowDialog(
            "To make a baby happy, we pick them up and kick them! Just remember to catch them or kick them into a " +
            "station, otherwise they will take damage.");
        yield return ShowDialog(
            "Try kicking the baby into the recharge station on the left. Hold down the kick button to charge, use " +
            "movement keys to aim, and release to kick.");
        
        yield return WaitUntilBabyInRechargeStation();
        
        yield return new WaitForSeconds(0.5f);
        yield return ShowDialog(
            "While we're waiting for the baby to charge, we might need to refill its oil later. " +
            "Interact with the station on the bottom right to get an oil bottle.");

        yield return WaitUntilBottlePickedUp();
        yield return ShowDialog("We can pick up, put down, and throw bottles just like babies.");
        yield return ShowDialog(
            "When the baby has finished charging, take it out of the charging station and interact with it while " +
            "holding the oil bottle. This gives the baby the bottle and it will replenish oil.");

        yield return new WaitUntil(() => babyState.currentOil >= babyState.oil * 0.95f);
        
        // Start health tutorial, if needed
        if (!healthTutorialStarted) {
            StopCoroutine(StartHealthTutorialIfBodyPartDetached());

            yield return ShowDialog(
                "Hmm... since the parents are not here yet, let's test the durability of the babies. Try kicking " +
                "the baby and make it land on the ground.");
            yield return new WaitUntil(() => bodyPartDetached);
            
            yield return HealthTutorial();
        }
        else if (!healthTutorialFinished)
        {
            yield return ShowDialog(
                "We should probably finish repairing the baby before the parent returns. Bring it and its missing " +
                "body part to the repair station, and hold down the Kick/Fix button.");
        }
        yield return new WaitUntil(() => healthTutorialFinished);

        yield return new WaitForSeconds(1);

        yield return ShowDialog(
            "Keep tending to the baby's needs until the parent returns. Try to keep them as high as possible to get " +
            "more stars!");

        // nice hack
        CheckForObject.firstTimeChecked = false;
        yield return new WaitUntil(() => CheckForObject.firstTimeChecked);
        
        yield return ShowDialog(
            "The parent is here! We should return the baby. To do that, pick up the baby and interact with the " +
            "parent. Make sure the baby's needs are satisfied!");

        var parentController = FindObjectOfType<ParentController>();
        yield return new WaitUntil(() => parentController.state.pickedUpObject != null);
        
        yield return ShowDialog(
            "You completed the tutorial! In an actual level, there will be more parents/babies and also more " +
            "obstacles. Good luck!");
    }

    private IEnumerator HealthTutorial()
    {
        healthTutorialStarted = true;
        
        yield return ShowDialog(
            "Oops! The baby had a hard landing and is now missing a body part. We should repair it before " +
            "giving it back to its parent.");
        yield return ShowDialog("First, pick up the baby and put it in the repair station, on the right.");

        yield return WaitUntilBabyInRepairStation();

        yield return ShowDialog("Go pick up the detached body parts and put them in the repair station.");

        yield return new WaitUntil(() => babyState.healthcap >= babyState.health);

        yield return ShowDialog(
            "Now that the body part is reattached, we can repair the baby to full health again. " +
            "Hold down the Kick/Fix button to do that.");

        yield return new WaitUntil(() => babyState.currentHealth >= babyState.health * 0.95f);

        yield return ShowDialog("Nice! The parent should be happy with the baby's health now.");
        
        healthTutorialFinished = true;
    }

    private IEnumerator StartHealthTutorialIfBodyPartDetached()
    {
        yield return new WaitUntil(() => bodyPartDetached);
        yield return HealthTutorial();
    }

    private BabyController baby;
    private BabyState babyState;
    private IEnumerator WaitUntilBabyFound()
    {
        while (!baby)
        {
            baby = FindObjectOfType<BabyController>();
            yield return new WaitForSeconds(0.1f);
        }
        
        // Init baby
        var interactable = baby.GetComponent<BabyPickUpInteractable>();
        interactable.HandlePickedUp += () => babyPickedUp = true;
        interactable.HandleKicked += () => babyKicked = true;
        baby.HandleBodyPartDetached += () => bodyPartDetached = true;
        babyState = baby.GetComponent<BabyState>();
    }

    private bool babyPickedUp = false;
    private IEnumerator WaitUntilBabyPickedUp()
    {
        babyPickedUp = false;
        yield return new WaitUntil(() => babyPickedUp);
    }

    private bool babyKicked = false;
    private IEnumerator WaitUntilBabyKicked()
    {
        babyKicked = false;
        yield return new WaitUntil(() => babyKicked);
    }

    private bool inDiaperStation;
    private IEnumerator WaitUntilBabyInDiaperStation()
    {
        inDiaperStation = false;
        yield return new WaitUntil(() => inDiaperStation);
    }

    private bool inRechargeStation;
    private IEnumerator WaitUntilBabyInRechargeStation()
    {
        inRechargeStation = false;
        yield return new WaitUntil(() => inRechargeStation);
    }
    
    private bool inRepairStation;
    private IEnumerator WaitUntilBabyInRepairStation()
    {
        inRepairStation = false;
        yield return new WaitUntil(() => inRepairStation);
    }
    
    private bool bodyPartDetached;

    private BottleInteractable bottle;
    
    private IEnumerator WaitUntilBottlePickedUp()
    {
        bottle = null;
        while (!bottle)
        {
            bottle = FindObjectOfType<BottleInteractable>();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ShowDialog(string message)
    {
        SetPause(true);
        dialogContainer.SetActive(true);
        dialogText.SetText(message);
        yield return PollInteract();
        dialogContainer.SetActive(false);
        SetPause(false);
    }

    private bool interacted = false;
    private void SetInteracted()
    {
        interacted = true;
    }
    
    private IEnumerator PollInteract()
    {
        foreach (var c in playerControllers)
        {
            c.InteractPoll += SetInteracted;
        }
        
        interacted = false;
        yield return new WaitUntil(() => interacted);
        
        foreach (var c in playerControllers)
        {
            c.InteractPoll -= SetInteracted;
        }
    }

    private void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }
}
