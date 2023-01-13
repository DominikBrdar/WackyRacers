using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform checkpoints;

    float d1, d2;
    int current = 0, next = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        d1 = Vector3.Distance(this.transform.position, checkpoints.GetChild(current).position);
        d2 = Vector3.Distance(this.transform.position, checkpoints.GetChild(next).position);
        if (d1 < d2)
        {
            if (d2 < 30)
            {
                current = next;
                if (next == checkpoints.childCount)
                {
                    next = 0;
                    Debug.Log("presao si krug");
                }
                else next++;
                Debug.Log(current + ". dio staze");
            }
        }
    }
}
