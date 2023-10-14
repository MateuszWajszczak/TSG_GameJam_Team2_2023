using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public enum ObjectFunctions
    {
        Key,
        Button,
        Collectible,
        Challenge,
        Flashlight,
        FinalDoor,
        // Add more values as needed
    }

    //Property of all interactives
    public float currentPickUpTime = 0f;
    public float maxPickUpTime = 0.1f;
    public bool pickUpAvailalbe;
    public string pickUpObjectName;

    //Indexes for Different Objects
    public int keyIndex;
    public int collectibleIndex;
    public GameObject targetChallenge;

    private CoreManager myManager;

    public ObjectFunctions myObjectFunction = ObjectFunctions.Key;

    private void Start()
    {
        currentPickUpTime = 0f;
        pickUpAvailalbe = false;

        //Clear to avoid bugs
        myManager = FindObjectOfType<CoreManager>();   
        if (myObjectFunction != ObjectFunctions.Key )
        {
            keyIndex = 0;
        }
        if (myObjectFunction != ObjectFunctions.Collectible)
        {
            collectibleIndex = 0;
        }
        if (myObjectFunction != ObjectFunctions.Challenge)
        {
            targetChallenge = null;
        }
    }

    private void Update()
    {
        if (currentPickUpTime > 0)
        {
            currentPickUpTime -= 1 * Time.deltaTime;
            if ((pickUpAvailalbe == false) && (currentPickUpTime > 0))
            {
                pickUpAvailalbe = true;
                UpdateTextAvailable();
            }
        }
        else
        {
            if (pickUpAvailalbe == true)
            {
                UpdateTextNotAvailable();
            }
            pickUpAvailalbe = false;
        }
    }

    public void UpdateTextAvailable()
    {
        myManager.currentObject = this;
        myManager.UpdatePickUpObjectText();
    }

    public void UpdateTextNotAvailable()
    {
        myManager.currentObject = null;
        myManager.UpdatePickUpObjectText();
    }

    public void InteractWithObject()
    {
        switch (myObjectFunction)
        {
            case ObjectFunctions.Key:
                myManager.CollectKey(keyIndex);
                UpdateTextNotAvailable();
                Destroy(this.gameObject);
                myManager.TeleportPlayerToHub();
                break;

            case ObjectFunctions.Challenge:
                myManager.BeginChallenge(targetChallenge.GetComponent<ChallengeObject>());
                break;

            case ObjectFunctions.Collectible:
                myManager.CollectColectible(collectibleIndex);
                UpdateTextNotAvailable();
                Destroy(this.gameObject);
                break;

            case ObjectFunctions.Flashlight:
                myManager.EnableFlashlight();
                UpdateTextNotAvailable();
                Destroy(this.gameObject);
                break;

            case ObjectFunctions.FinalDoor:
                int keys = 0;
                if (myManager.keyStatus.ContainsKey(1) && myManager.keyStatus[1] == true)
                {
                    keys += 1;
                }
                if (myManager.keyStatus.ContainsKey(2) && myManager.keyStatus[2] == true)
                {
                    keys += 1;
                }  
                if (myManager.keyStatus.ContainsKey(3) && myManager.keyStatus[3] == true)
                {
                    keys += 1;
                }
                if (myManager.keyStatus.ContainsKey(4) && myManager.keyStatus[4] == true)
                {
                    keys += 1;
                }
                if (keys >= 4)
                {
                    myManager.winGame();
                }
                break;
        }
    }
}
