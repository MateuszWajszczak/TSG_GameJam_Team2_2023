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
        // Add more values as needed
    }

    public float currentPickUpTime = 0f;
    public float maxPickUpTime = 0.1f;
    public bool pickUpAvailalbe;
    public string pickUpObjectName;
    public int keyIndex;
    public int collectibleIndex;

    private CoreManager myManager;

    [SerializeField] private ObjectFunctions myObjectFunction = ObjectFunctions.Key;

    private void Start()
    {
        currentPickUpTime = 0f;
        pickUpAvailalbe = false;
        myManager = FindObjectOfType<CoreManager>();   
        if (myObjectFunction != ObjectFunctions.Key )
        {
            keyIndex = 0;
        }
        if (myObjectFunction != ObjectFunctions.Collectible)
        {
            collectibleIndex = 0;
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
                break;

            case ObjectFunctions.Button:
                break;

            case ObjectFunctions.Collectible:
                myManager.CollectColectible(collectibleIndex);
                UpdateTextNotAvailable();
                Destroy(this.gameObject);
                break;
        }
    }
}
