using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime;
    public int startingTime = 5;
    private GameObject player;

    bool freeze = true;
    float originalDrag;

    [SerializeField] Text countdownText;
    void Start()
    {
        currentTime = startingTime;
        player = GameObject.FindGameObjectWithTag("PlayerCont");
        originalDrag = player.GetComponent<Rigidbody>().drag;
        InvokeRepeating("Count", 1.0f, 1.0f);
    }

    private void Update()
    {
        if (freeze)
        {
            player.GetComponent<Rigidbody>().drag = 9999;
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
            player.GetComponent<Rigidbody>().drag = originalDrag;
        }
    }

    void Hide()
    {
        countdownText.text = string.Format("");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}