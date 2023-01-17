using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public NetworkDebugStart Starter;
    public NetworkRunner netRunner;
    public Button StartButton;
    public Button ExitButton;
    public Transform checkpoints;
    public Transform player;
    public Transform StartArch;
    public Transform FinishLine;
    public Canvas CanvasUI;
    public Canvas canvasLoading;
    public Text Place;
    public Text Lap;
    public int NumberOfLaps;

    private LinkedList<GameObject> Cars;

    float d1, d2, d3, d4;
    int current = 0, next = 1;
    int place = 0, lap = 1;

    // Start is called before the first frame update
    void Start()
    {
        // set number of laps
        
        Lap.transform.GetChild(0).GetComponent<Text>().text = "/" + NumberOfLaps;
        Lap.text = "" + lap;

        StartButton.onClick.AddListener(startGame);

        ExitButton.onClick.AddListener(() => Application.Quit());
        //useUICanvas(false);
        CanvasUI.enabled = false;
        canvasLoading.enabled = true;
        // TODO (dominik) initial place (depends on other cars)
        Cars = new LinkedList<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        Debug.Log("number of cars = " + Cars.Count);
    }

    void startGame()
    {
        /* TODO Stvaranje protivnika, nije dovrseno
        EntryBehaviour[] entryB = Content.GetComponentsInChildren<EntryBehaviour>();
        enemiesContainer = new GameObject("Enemies");
        enemiesContainer.transform.SetParent(Simulation.transform);
        enemiesContainer.SetActive(true);

        for (int i = 0; i < entryB.Length; i++)
        {
            // inicijalizacija puta, ako put nije zadan -> zanemari objekt
            if (entryB[i].path.Count < 1) continue;
            var enemy = Instantiate(Enemy);
            enemy.transform.SetParent(enemiesContainer.transform);
            enemy.GetComponent<EnemyBehaviour>().path = entryB[i].path;

            // inicijalizacija modela
            Instantiate(EnemyModels.transform.GetChild(entryB[i].Model.value).gameObject).transform.SetParent(enemy.transform);
            enemy.transform.position = entryB[i].path[0];

            enemy.SetActive(true);
        }
        */

        //useUICanvas(false);
        /*
        player.GetComponent<Rigidbody>().ResetInertiaTensor();
        player.position = new Vector3(22, 0, 22);
        player.rotation = new Quaternion(0, 1, 0, 1);
        */
        Starter.Shutdown();
        // TODO pokreni utrku
        //

    }

    public void useUICanvas(bool b)
    {
        if (b)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        CanvasUI.enabled = b;
        Cursor.visible = b;
        canvasLoading.enabled = !b;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.position.y < -10)
        {
            returnToTrack();
        }
        // TODO (dominik) dodati return to track kada se stisne i drzi space

        d1 = Vector3.Distance(player.position, checkpoints.GetChild(current).position);
        d2 = Vector3.Distance(player.position, checkpoints.GetChild(next).position);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            useUICanvas(true);
        }


    }

    void returnToTrack()
    {
        player.GetComponent<Rigidbody>().ResetInertiaTensor();
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.position = checkpoints.GetChild(current).position;
        player.rotation = checkpoints.GetChild(current).rotation;
        player.GetComponent<Rigidbody>().isKinematic = false;
    }

    void updatePlace()
    {
        // cini se da ovo nije dovoljno, postoje sluèajevi gdje netko vodi za cijeli krug
        // treba provjeriti prvo krug, onda checkpoint i onda ovo
        // TODO (dominik)
        place = 1;
        
        Transform t;
        foreach(GameObject c in Cars)
        {
            d3 = Vector3.Distance(c.transform.position, checkpoints.GetChild(current).position);
            d4 = Vector3.Distance(c.transform.position, checkpoints.GetChild(next).position);
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


