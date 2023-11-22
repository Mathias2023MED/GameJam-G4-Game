using UnityEngine;
using UnityEngine.UI;

public class EnableOnButtonClick : MonoBehaviour
{
    // Public GameObject to enable
    public GameObject targetGameObject;

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
        // Enable the target GameObject
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Target GameObject not assigned!");
        }
    }
}