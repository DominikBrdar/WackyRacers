using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupStatus : MonoBehaviour
{
    public float speedModifier = 1.0f;
    private PrometeoCarController pcc;

    public const int CONST_DEFAULT_MAX_SPEED = 90;
    public const int CONST_DEFAULT_ACC_MULTIPLIER = 6;
    public const float CONST_DEFAULT_STEER_SPEED = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        speedModifier = 1;
        InvokeRepeating("Decay", 1.0f, 1.0f);
        pcc = GetComponent<PrometeoCarController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Decay()
    {
        if (speedModifier > 1.0f)
        {
            speedModifier -= 0.1f;
        }
        if (speedModifier < 1.0f)
        {
            speedModifier += 0.1f;
        }
        UpdateController();
    }

    void UpdateController()
    {
        pcc.maxSpeed = (int)(CONST_DEFAULT_MAX_SPEED * speedModifier);
        pcc.accelerationMultiplier = (int)(CONST_DEFAULT_ACC_MULTIPLIER * speedModifier);
        pcc.steeringSpeed = (CONST_DEFAULT_STEER_SPEED * speedModifier);
    }

    public void Increment()
    {
        speedModifier += 1.0f;
        UpdateController();
    }

    public void Decrement()
    {
        speedModifier -= 1.0f;
        UpdateController();
    }
}
