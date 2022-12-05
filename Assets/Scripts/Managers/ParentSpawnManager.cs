using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParentSpawnManager : MonoBehaviour
{
    public GameObject parent;
    public GameObject childContainer;
    public Transform start;
    // randomly choice parent and child 
    // NOTE: be careful when assigning material in Editor the order of the list is used to determine
    // which child/parent is assigned to which material (Parent Texture length === Child Texture length)
    public List<Texture2D> parentTexture;
    public Texture2D parentTextureEyes;
    public List<Texture2D> childTexture;
    public Texture2D childTextureEyes;
    // time between spawning each parent
    public float delayTime = 2f;
    public List<string> childNames;
    public GameObject floor;
    public List<Transform> lineUpPoints;
    public Transform waitPoint;
    public Transform exitPoint;

    public virtual int NumberOfParents => 4 + (LevelsManager.Instance.Level - 1);
    private BehaviorExecutor behaviorExecutorParent;
    // track all babies in the game
    private List<GameObject> children = new();
    // parents in line 
    private Queue<GameObject> parentsInLine = new();
    // parents waiting to get in line
    private Queue<GameObject> parentsWaiting = new();
    private Queue<GameObject> parentsWaitingForPickup = new();
    

    protected virtual void Start()
    {
        childNames = new List<string>() { "Bob", "Anna", "Gaston", "Lemmy", "Chad", "Linda", "Bruce", "Penelope", "Jillian", "Carter" };
        StartCoroutine(SpawnMultipleParents());
        if (parentTexture.Count != childTexture.Count)
        {
            Debug.LogError("Parent and Child Texture lists must be the same length");
        }
    }

    private void Update()
    {
        Debug.Log($"Parents in line: {parentsInLine.Count}");
        if (parentsInLine.Count > 0)
        {
            var parentAtFront = parentsInLine.Peek();
            var parentState = parentAtFront.GetComponent<ParentState>();
            // if parents is leaving they should have the baby
            if (parentState.isLeaving && parentState.pickedUpObject != null)
            {
                parentsInLine.Dequeue();
            }
            // if parent is waiting and they should wait until there are no babies
            else if (!parentState.isLeaving && parentState.pickedUpObject == null)
            {
                parentsWaitingForPickup.Enqueue(parentsInLine.Dequeue());
                parentState.currentTargetPoint = parentState.waitPoint;
                parentState.frontOfQueue = false;
            }
        }

        // queue parents waiting for pickup
        if (parentsWaitingForPickup.Count > 0)
        {
            var parentsWaitingForPickUp = parentsWaitingForPickup.Peek();
            var parentWaitingForPickup = parentsWaitingForPickUp.GetComponent<ParentState>();
            if (parentWaitingForPickup.readyForPickUp)
            {
                parentsWaiting.Enqueue(parentsWaitingForPickup.Dequeue());
            }
        }
        Debug.Log($"Parents waiting for pick up: {parentsWaitingForPickup.Count}");
        Debug.Log($"Parents waiting: {parentsWaiting.Count}");
        // queue the parents waiting
        while (parentsInLine.Count < lineUpPoints.Count && parentsWaiting.TryPeek(out _))
        {
            parentsInLine.Enqueue(parentsWaiting.Dequeue());
        }
        
        // update target point for each parent
        for (int i = 0; i < parentsInLine.Count; i++)
        {
            var parentInLine = parentsInLine.ElementAt(i);
            var parentInLineState = parentInLine.GetComponent<ParentState>();
            // NOTE: magic number 2 is the height of the parent (approximately)
            parentInLineState.frontOfQueue = i == 0 && Vector3.Distance(lineUpPoints[0].position, parentInLine.transform.position) < 2f;
            parentInLineState.currentTargetPoint = lineUpPoints[i].position;
        }
    }

    protected IEnumerator SpawnMultipleParents()
    {
        // Shuffle the list of textures so each parent/child gets a unique one
        var rng = new System.Random();
        var randomIndices = Enumerable.Range(0, parentTexture.Count).OrderBy(a => rng.Next()).ToList();
        
        for (var i = 0; i < NumberOfParents; i++)
        {
            float delayRandom = Random.Range(1f, delayTime);//You can change parents spawn time here
            yield return new WaitForSeconds(delayRandom);
            var customWaitPoint = new Vector3(waitPoint.position.x + i * 2, waitPoint.position.y, waitPoint.position.z);
            if (i < lineUpPoints.Count)
            {
                SpawnParent(customWaitPoint, lineUpPoints[i].position, childNames[i], randomIndices[i], parentsInLine);
            }
            else
            {
                SpawnParent( customWaitPoint, customWaitPoint, childNames[i], randomIndices[i], parentsWaiting);
            }
        }
        // loop over each child
        foreach (GameObject child in children)
        {
            var state = child.GetComponent<BabyState>();
            if (state != null)
            {
                List<GameObject> peers = children.Where(c => c.GetInstanceID() != child.GetInstanceID()).ToList();
                state.peers = peers;
            }
        }
    }
    
    private void SpawnParent(Vector3 leavePoint, Vector3 targetPoint, string childName, int randomIndex, Queue<GameObject> queue)
    {
        //randomize the color, parent and child will have same color
        var parentInstance = Instantiate(parent, start.position, Quaternion.identity);
        queue.Enqueue(parentInstance);
        var childInstance = Instantiate(childContainer, Vector3.zero, Quaternion.identity);
        var childState = childInstance.GetComponent<BabyState>();
        var parentState = parentInstance.GetComponent<ParentState>();
        var interactable = childInstance.GetComponent<BabyPickUpInteractable>();
        var parentInteractable = parentInstance.GetComponent<ParentInteractable>();
        var childController = childInstance.GetComponent<BabyController>();
        // the floor is used to determine if the baby is on the ground or not
        childController.Floor = floor;
        // assign the child name
        var parentMat = new Material(Shader.Find("Custom/BlendShader"));
        var childMat = new Material(Shader.Find("Custom/BlendShader"));
        // assign the parent and child textures randomly
        var randomParentTexture = parentTexture[randomIndex];
        var randomChildTexture = childTexture[randomIndex];
        // set the parameters of the material
        parentMat.SetTexture("_MainTex", randomParentTexture);
        parentMat.SetTexture("_Decal", parentTextureEyes);
        childMat.SetTexture("_MainTex", randomChildTexture);
        childMat.SetTexture("_Decal", childTextureEyes);
        
        // Pick a random, saturated and not-too-dark color
        var randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);;
        parentMat.SetColor("_Color", randomColor);
        childMat.SetColor("_Color", randomColor);
        
        // assign the material to the parent and child
        parentInstance.GetComponentInChildren<Renderer>().material = parentMat;
        foreach (var childRenderer in childInstance.GetComponentsInChildren<Renderer>())
            childRenderer.material = childMat;
        
        // invoke the outline recalculate method to account for the extra gameobject added
        // TODO: this is hack to get the outline to work, need to find a better way
        var outline = childInstance.GetComponent<Outline>();
        outline.Recalculate();
        outline.enabled = false;
        outline.enabled = true;
        
        // Programmatically make the parent pick up the child
        //TODO: investigate how to refactor ParentInteractable into something else other than a station interactable
        interactable.PickUp(parentInstance.GetComponent<AgentState>());
        interactable.inStation = true;
        parentInteractable.pickedUpObject = interactable;
        
        parentInstance.GetComponent<ParentState>().childId = childInstance.GetInstanceID();
        childState.name = childName;
        parentState.frontOfQueue = false;
        parentState.currentTargetPoint = targetPoint;
        parentState.waitPoint = leavePoint;
        behaviorExecutorParent = parentInstance.GetComponent<BehaviorExecutor>();
        if (behaviorExecutorParent != null)
        {
            behaviorExecutorParent.SetBehaviorParam("state", parentState);
            behaviorExecutorParent.SetBehaviorParam("LeavePoint", leavePoint);
            behaviorExecutorParent.SetBehaviorParam("ExitPoint", exitPoint.position);
        }
        var behaviorExecutorChild = childInstance.GetComponent<BehaviorExecutor>();
        if (behaviorExecutorChild != null)
        {
            behaviorExecutorChild.SetBehaviorParam("wanderArea", GameObject.Find("Floor"));
        }
        children.Add(childInstance);
    }
}