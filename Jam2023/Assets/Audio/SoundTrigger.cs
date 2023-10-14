using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioSource soundSource;
    public AudioClip soundToPlay;

    private bool hasPlayed;

    private void Start()
    {
        hasPlayed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true; // To ensure the sound is played only once

            // Play the sound
            soundSource.PlayOneShot(soundToPlay);

            // Schedule the trigger volume to be destroyed after the sound duration
            Destroy(gameObject, soundToPlay.length);
        }
    }
}
