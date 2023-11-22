using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlaySoundOnButtonClick : MonoBehaviour
{
    // Public audio clip to play
    public AudioClip soundClip;

    private Button button;
    private AudioSource audioSource;

    void Start()
    {
        // Ensure there's an audio clip assigned
        if (soundClip == null)
        {
            Debug.LogError("Audio Clip not assigned!");
            return;
        }

        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();

        // Ensure there's a Button component
        if (button == null)
        {
            Debug.LogError("Button component not found!");
            return;
        }

        // Add a click listener to the button
        button.onClick.AddListener(OnButtonClick);

        // Add an AudioSource component to play the sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
    }

    void OnButtonClick()
    {
        // Play the assigned sound clip
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}