using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] GameObject guiObject;
    [SerializeField] GameObject panel;

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartClicked);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartClicked);
    }

    private void StartClicked()
    {
        guiObject.SetActive(true);
        panel.SetActive(false);
    }
}
