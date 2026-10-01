using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Tooltip("The maximum value of health")]
    [SerializeField] private float maxHealth;

    [Tooltip("How much health per secondes the fire gain when not extinguished")]
    [SerializeField] private float healthIncreasePerSec;


    [Tooltip("What scale to apply according to the health")]
    [SerializeField] private float scalePerHealth;

    [SerializeField] private float health;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            UpdateHealth();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
    }

    void UpdateHealth()
    {
        // activate/deactivate gameObject if health > 0
        // TODO


        // Gain health if fire is not fully extinguished
        // TODO

        // Update scale according to Health
        // TODO

    }
}