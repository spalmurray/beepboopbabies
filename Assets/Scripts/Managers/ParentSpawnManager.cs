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
    // basic baby to add accessories to
    public GameObject baseBaby;
    // list of accessories to add to the baby
    public List<GameObject> HairAccessories;
    public List<GameObject> ChestAccessories;
    // time between spawning each parent
    public float delayTime = 2f;

    public List<string> childNames;

    private BehaviorExecutor behaviorExecutor;

    public int NumberOfParents => leavePoints.Count;

    private void Start()
    {
        childNames = new List<string>() { "Bob", "Anna", "Gaston", "Lemmy" };
        StartCoroutine(SpawnMultipleParents());
        if (parentTexture.Count != childTexture.Count)
        {
            Debug.LogError("Parent and Child Texture lists must be the same length");
        }
    }

    private IEnumerator SpawnMultipleParents()
    {
        for (var i = 0; i < NumberOfParents; i++)
        {
            float delayRandom = Random.Range(1f, delayTime);//You can change parents spawn time here
            yield return new WaitForSeconds(delayRandom);
            SpawnParent(arrivePoints[i].position, leavePoints[i].position, childNames[i]);
        }
    }
    
    private void AddAccesory(List<GameObject> accessories, GameObject baseBaby)
    {
        int randomIndex = Random.Range(0, accessories.Count + 1);
        // if random index is 0 don't add teh accessory
        if (randomIndex == 0)
        {
            return;
        }
        // otherwise add the accessory
        randomIndex -= 1;
        
        GameObject accessory = Instantiate(accessories[randomIndex], Vector3.zero, Quaternion.identity);
        accessory.transform.SetParent(baseBaby.transform);
    }

    private void SpawnParent(Vector3 arrivePoint, Vector3 leavePoint, string childName)
    {

        //randomize the color, parent and child will have same color
        var parentInstance = Instantiate(parent, start.position, Quaternion.identity);
        var childInstance = Instantiate(childContainer, Vector3.zero, Quaternion.identity);
        var childState = childInstance.GetComponent<BabyState>();
        var interactable = childInstance.GetComponent<PickUpInteractable>();
        // instaniate base baby
        var baseBabyObject = Instantiate(baseBaby, Vector3.zero, Quaternion.identity);
        
        // assign the child name
        var parentMat = new Material(Shader.Find("Custom/BlendShader"));
        var childMat = new Material(Shader.Find("Custom/BlendShader"));
        // assign the parent and child textures randomly
        var randomIndex = Random.Range(0, parentTexture.Count);
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
        
        // add the accessories
        AddAccesory(HairAccessories, baseBabyObject);
        AddAccesory(ChestAccessories, baseBabyObject);
        
        // assign child base to child container
        baseBabyObject.transform.SetParent(childInstance.transform);
        
        // assign the material to the parent and child
        parentInstance.GetComponentInChildren<Renderer>().material = parentMat;
        foreach (var childRenderer in childInstance.GetComponentsInChildren<Renderer>())
            childRenderer.material = childMat;
        
        // invoke the outline recalcuate method to account for the extra gameobject added
        // TODO: this is hack to get the outline to work, need to find a better way
        var outline = childInstance.GetComponent<Outline>();
        outline.Recalculate();
        outline.enabled = false;
        outline.enabled = true;
        
        // Programmatically make the parent pick up the child
        interactable.PickUp(parentInstance.GetComponent<AgentState>());
        parentInstance.GetComponent<ParentState>().childId = childInstance.GetInstanceID();
        childState.name = childName;
        behaviorExecutor = parentInstance.GetComponent<BehaviorExecutor>();
        if (behaviorExecutor != null)
        {
            behaviorExecutor.SetBehaviorParam("LeavePoint", leavePoint);
            behaviorExecutor.SetBehaviorParam("ArrivePoint", arrivePoint);
        }
    }
}