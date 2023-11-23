using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

    private AudioSource audioSource;

    [SerializeField] private List<AudioClip> punchSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> kickSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> blockSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> jumpSounds = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PunchSound()
    {
        var rand = Random.Range(0, punchSounds.Count);
        audioSource.PlayOneShot(punchSounds[rand]);
    }
    public void KickSound()
    {
        var rand = Random.Range(0, kickSounds.Count);
        audioSource.PlayOneShot(kickSounds[rand]);
    }
    public void BlockSound()
    {
        var rand = Random.Range(0, blockSounds.Count);
        audioSource.PlayOneShot(blockSounds[rand]);
    }
    public void JumpSound()
    {
        var rand = Random.Range(0, jumpSounds.Count);
        audioSource.PlayOneShot(jumpSounds[rand]);
    }
}
