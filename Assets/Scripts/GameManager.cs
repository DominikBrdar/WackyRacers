using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Transform checkpoints;
    public Transform cars;
    public Canvas canvas;
    public Text Place;
    public Text Lap;
    public int NumberOfLaps;

    float d1, d2, d3, d4;
    int current = 0, next = 1;
    int place = 0, lap = 1;

    // Start is called before the first frame update
    void Start()
    {
        // set number of laps
        Lap.transform.GetChild(0).GetComponent<Text>().text = "/" + NumberOfLaps;
        Lap.text = "" + lap;
        // TODO (dominik) initial place (depends on other cars)
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -10)
        {
            returnToTrack();
        }
        // TODO (dominik) dodati return to track kada se stisne i drzi space

        d1 = Vector3.Distance(this.transform.position, checkpoints.GetChild(current).position);
        d2 = Vector3.Distance(this.transform.position, checkpoints.GetChild(next).position);
        if (d1 < d2)
        {
            if (d2 < 24)
            {
                current = next;
                next++;
                if (next == checkpoints.childCount)
                {
                    next = 0;
                    Debug.Log("presao si krug");
                    if (++lap > NumberOfLaps)
                    {
                        //TODO (dominik) utrka zavrsena
                    }
                    else
                    {
                        Lap.text = "" + lap;
                    }
                }
                
                Debug.Log(current + ". dio staze");
            }
        }

        updatePlace();
        
    }

    void returnToTrack()
    {
        GetComponent<Rigidbody>().ResetInertiaTensor();
        GetComponent<Rigidbody>().isKinematic = true;
        this.transform.position = checkpoints.GetChild(current).position;
        this.transform.rotation = checkpoints.GetChild(current).rotation;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void updatePlace()
    {
        // cini se da ovo nije dovoljno, postoje sluèajevi gdje netko vodi za cijeli krug
        // treba provjeriti prvo krug, onda checkpoint i onda ovo
        // TODO (dominik)
        place = 1;
        foreach (Transform t in cars)
        {
            d3 = Vector3.Distance(t.position, checkpoints.GetChild(current).position);
            d4 = Vector3.Distance(t.position, checkpoints.GetChild(next).position);
            if (d4 < d3)
            {
                place++;
            }
            else if (d4 < d2)
            {
                place++;
            }
        }
        if (place == 1) {
            Place.text = "1st";
        } else if (place == 2) {
            Place.text = "2nd";
        } else if (place == 3) {
            Place.text = "3rd";
        } else {
            Place.text = "" + place + "th";
        }
    }
}


