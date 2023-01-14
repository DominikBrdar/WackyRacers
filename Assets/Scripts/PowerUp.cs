using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
        
    }

    void Pickup(Collider player)
    {
        //Debug.Log("Powerup working");

        // effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        // apply effect
        PickupStatus status = player.GetComponentInParent<PickupStatus>();
        if (status is not null)
        {
            status.Increment();
        }

        // destory powerup
        Destroy(gameObject);
    }
}
