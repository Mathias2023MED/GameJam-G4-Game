using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Swap : MonoBehaviour
{
    // Public lists for GameObjects to disable and enable
    public List<GameObject> elementsToDisable;
    public List<GameObject> elementsToEnable;

    // Public UI Button to trigger the action
    public Button actionButton;

    void Start()
    {
        // Ensure the button has a click listener
        if (actionButton != null)
        {
            actionButton.onClick.AddListener(OnActionButtonClick);
        }
        else
        {
            Debug.LogError("Action Button not assigned!");
        }
    }

    void OnActionButtonClick()
    {
        // Disable the list of UI elements
        foreach (GameObject element in elementsToDisable)
        {
            if (element != null)
            {
                element.SetActive(false);
            }
            else
            {
                Debug.LogWarning("One of the elements to disable is not assigned!");
            }
        }

        // Enable the list of UI elements
        foreach (GameObject element in elementsToEnable)
        {
            if (element != null)
            {
                element.SetActive(true);
            }
            else
            {
                Debug.LogWarning("One of the elements to enable is not assigned!");
            }
        }
    }
}