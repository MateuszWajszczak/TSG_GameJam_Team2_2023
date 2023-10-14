using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DarkToggle;

public class PortalUnlockRequirements : MonoBehaviour
{
    public enum UnlockType
    {
        UnlockOnFlashlight,
        UnlockOnLast,
        // Add more values as needed
    }

    [SerializeField] private CoreManager myManager;
    [SerializeField] private GameObject myPortal;
    public int myPortalID;
    public bool completeted = false;
    public UnlockType myUnlockType;

    private void Start()
    {
        myManager = FindObjectOfType<CoreManager>();
        myPortal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((myPortal.activeSelf == false) && (completeted == false))
        {
            switch (myUnlockType)
            {
                case UnlockType.UnlockOnFlashlight:
                    if (myManager.flashlightCollected)
                    {
                        myPortal.SetActive(true);
                    }
                    break;

                case UnlockType.UnlockOnLast:
                    if ((myManager.keyStatus.ContainsKey(1) && myManager.keyStatus[1] == true) && (myManager.keyStatus.ContainsKey(2) && myManager.keyStatus[2] == true)
                        && (myManager.keyStatus.ContainsKey(3) && myManager.keyStatus[3] == true))
                    {
                        myPortal.SetActive(true);
                    }
                    break;
                    // Add more cases for additional enum values
            }
        }
        if (myManager.keyStatus.ContainsKey(myPortalID) && myManager.keyStatus[myPortalID] == true)
        {
            completeted = true;
            myPortal.SetActive(false);
        }
    }
}
