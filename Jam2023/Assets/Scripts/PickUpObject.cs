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
        // Add more values as needed
    }

    public float currentPickUpTime = 0f;
    public float maxPickUpTime = 0.1f;
    public bool pickUpAvailalbe;
    public string pickUpObjectName;
    public int keyIndex;

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
                break;

            case ObjectFunctions.Button:
                break;

                // Add more cases for additional enum values
        }
    }
}
