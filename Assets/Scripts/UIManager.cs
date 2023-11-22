using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Public UI element to enable
    public GameObject targetUIElement;

    // List of UI elements to disable
    public List<GameObject> elementsToDisable;

    // Reference to the button that triggers the action
    public Button triggerButton;

    void Start()
    {
        // Ensure the button has a click listener
        if (triggerButton != null)
        {
            triggerButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button not assigned!");
        }
    }

    void OnButtonClick()
    {
        // Enable the target UI element
        if (targetUIElement != null)
        {
            targetUIElement.SetActive(true);
        }
        else
        {
            Debug.LogError("Target UI element not assigned!");
        }

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
    }
}
