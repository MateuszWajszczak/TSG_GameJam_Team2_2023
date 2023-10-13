using UnityEngine;

public class KillPlayerTrigger : MonoBehaviour
{
    public CoreManager myManager; // Reference to the CoreManager object

    private void Start()
    {
        myManager = FindObjectOfType<CoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the "Player" tag
        {
            if (myManager != null)
            {
                myManager.KillPlayerInChallenge();
            }
        }
    }
}
