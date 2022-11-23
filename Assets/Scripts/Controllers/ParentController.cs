using UnityEngine;
using UnityEngine.AI;


public class ParentController : MonoBehaviour
{
    
    public Animator anim;
    public NavMeshAgent agent;
    public ParentState state;

    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int HasBaby = Animator.StringToHash("HasBaby");
    
    // Update is called once per frame
    void Update()
    {
        anim.SetBool(Walking, agent.velocity.magnitude > 0.1f);
        anim.SetBool(HasBaby, state.pickedUpObject != null);
    }
}
