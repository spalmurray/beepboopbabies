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
    public List<Transform> leavePoints;
    public List<Transform> arrivePoints;
    // time between spawning each parent
    public float delayTime = 2f;

    public List<string> childNames;
    public GameObject floor;

    private BehaviorExecutor behaviorExecutorParent;

    public int NumberOfParents => Mathf.Min(2 + LevelsManager.Instance.Level, leavePoints.Count);
    // track all babies in the game
    public List<GameObject> children = new();

    private void Start()
    {
        childNames = new List<string>() { "Bob", "Anna", "Gaston", "Lemmy", "Temp1", "Temp2", "Temp3", "Temp4" };
        StartCoroutine(SpawnMultipleParents());
        if (parentTexture.Count != childTexture.Count)
        {
            Debug.LogError("Parent and Child Texture lists must be the same length");
        }
    }

    private IEnumerator SpawnMultipleParents()
    {
        // Shuffle the list of textures so each parent/child gets a unique one
        var rng = new System.Random();
        var randomIndices = Enumerable.Range(0, parentTexture.Count).OrderBy(a => rng.Next()).ToList();
        
        for (var i = 0; i < NumberOfParents; i++)
        {
            float delayRandom = Random.Range(1f, delayTime);//You can change parents spawn time here
            yield return new WaitForSeconds(delayRandom);
            SpawnParent(arrivePoints[i].position, leavePoints[i].position, childNames[i], randomIndices[i]);
        }
        // loop over each child
        Debug.Log("Setting peers");
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
    
    private void SpawnParent(Vector3 arrivePoint, Vector3 leavePoint, string childName, int randomIndex)
    {

        //randomize the color, parent and child will have same color
        var parentInstance = Instantiate(parent, start.position, Quaternion.identity);
        var childInstance = Instantiate(childContainer, Vector3.zero, Quaternion.identity);
        var childState = childInstance.GetComponent<BabyState>();
        var interactable = childInstance.GetComponent<PickUpInteractable>();
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
        interactable.PickUp(parentInstance.GetComponent<AgentState>());
        parentInstance.GetComponent<ParentState>().childId = childInstance.GetInstanceID();
        childState.name = childName;
        behaviorExecutorParent = parentInstance.GetComponent<BehaviorExecutor>();
        if (behaviorExecutorParent != null)
        {
            behaviorExecutorParent.SetBehaviorParam("LeavePoint", leavePoint);
            behaviorExecutorParent.SetBehaviorParam("ArrivePoint", arrivePoint);
        }
        var behaviorExecutorChild = childInstance.GetComponent<BehaviorExecutor>();
        if (behaviorExecutorChild != null)
        {
            behaviorExecutorChild.SetBehaviorParam("wanderArea", GameObject.Find("Floor"));
        }
        children.Add(childInstance);
    }
}