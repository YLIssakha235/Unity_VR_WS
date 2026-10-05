using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireGrenadeManager : MonoBehaviour
{
    [Header("Grenade Settings")]
    [Tooltip("Radius of the extinguishing explosion")]
    [SerializeField] private float explosionRadius = 3f;
    [Tooltip("Damage dealt to fires (should be high enough to instantly kill it)")]
    [SerializeField] private float extinguishingPower = 100f;

    // Variables from whiteboard
    private bool grenadeActivated = false;
    private bool onCollision = false;
    private float timer = 2.5f;

    private XRGrabInteractable xrGrab;

    void Start()
    {
        xrGrab = GetComponent<XRGrabInteractable>();

        // Link the XR events to the whiteboard methods
        xrGrab.activated.AddListener(Activated);
        xrGrab.selectExited.AddListener(OnGrenadeRelease);
    }

    // Triggered when pulling the VR trigger while holding the sphere
    private void Activated(ActivateEventArgs arg)
    {
        grenadeActivated = true;
        Debug.Log("Grenade Activated! Ready to throw.");
    }

    // Triggered when you let go of the sphere
    private void OnGrenadeRelease(SelectExitEventArgs arg)
    {
        // If activated before throwing, start the 2.5 second fuse timer
        if (grenadeActivated)
        {
            StartCoroutine(ExplosionTimer());
        }
    }

    private IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(timer);
        Explode();
    }

    // Triggered when the sphere physically hits something (like the floor or a fire)
    private void OnCollisionEnter(Collision collision)
    {
        onCollision = true;

        // Optional: Make it explode instantly on impact instead of waiting for the timer
        if (grenadeActivated)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Prevent multiple explosions if timer and collision happen at the exact same time
        if (!grenadeActivated) return;
        grenadeActivated = false;

        // 1. Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // 2. Check if any of them are Fires
        foreach (Collider hit in colliders)
        {
            Fire fire = hit.GetComponent<Fire>();
            if (fire != null)
            {
                // Instantly extinguish the fire by dropping its health
                fire.Health -= extinguishingPower;
            }
        }

        // 3. Destroy the grenade object after it pops
        Destroy(gameObject);
    }
}