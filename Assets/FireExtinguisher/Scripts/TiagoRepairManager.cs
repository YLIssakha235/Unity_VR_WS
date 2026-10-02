using UnityEngine;
using UnityEngine.AI;

public class TiagoRepairManager : MonoBehaviour
{
    [Header("References")]
    public GameObject robotHead;
    public Transform neckBone; // Drag torso_lift_link here
    public NavMeshAgent robotAgent;

    // We will trigger this function via the XR Socket Interactor
    public void OnHeadReattached()
    {
        // 1. Re-parent the head back to the neck structure
        robotHead.transform.SetParent(neckBone);

        // 2. Lock the physics so it doesn't flop around
        robotHead.GetComponent<Rigidbody>().isKinematic = true;

        // 3. Optional: Lock the head in place so the player can't rip it off again
        robotHead.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>().enabled = false;

        // 4. Resume the patrol
        if (robotAgent != null)
        {
            robotAgent.isStopped = false;
        }
    }
}