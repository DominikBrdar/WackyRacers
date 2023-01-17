using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime;
    public int startingTime = 5;
    private GameObject[] players;

    bool freeze = true;
    float originalDrag;

    [SerializeField] Text countdownText;
    void Start()
    {
        currentTime = startingTime;
        players = GameObject.FindGameObjectsWithTag("PlayerCont");
        originalDrag = players[0].GetComponent<Rigidbody>().drag;
        InvokeRepeating("Count", 1.0f, 1.0f);
    }

    private void Update()
    {
        if (freeze)
        {
            foreach (GameObject player in players)
            {
                player.GetComponent<Rigidbody>().drag = 9999;
            }
            
        }
        
    }

    void Count()
    {
        currentTime -= 1;
        countdownText.text = string.Format("THE RACE STARTS IN {0}", currentTime.ToString("0"));

        if (currentTime <= 0)
        {
            CancelInvoke("Count");
            countdownText.text = string.Format("GO", currentTime.ToString("0"));
            Invoke("Hide", 1.0f);

            // unfreeze players
            freeze = false;
            foreach (GameObject player in players)
            {
                player.GetComponent<Rigidbody>().drag = originalDrag;
            }
        }
    }

    void Hide()
    {
        countdownText.text = string.Format("");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}