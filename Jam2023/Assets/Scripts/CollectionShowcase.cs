using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DarkToggle;

public class CollectionShowcase : MonoBehaviour
{
    public enum CollectionType
    {
        Key,
        Collectible,
    }
    public int itemID = 0;
    public CollectionType myType;
    public CoreManager myManager;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        myManager = FindObjectOfType<CoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().enabled == false)
        {
            switch (myType)
            {
                case CollectionType.Key:
                    if (myManager.keyStatus.ContainsKey(itemID) && myManager.keyStatus[itemID] == true)
                    {
                        GetComponent<Renderer>().enabled = true;
                    }
                    break;

                case CollectionType.Collectible:
                    if (myManager.collectibleStatus.ContainsKey(itemID) && myManager.collectibleStatus[itemID] == true)
                    {
                        GetComponent<Renderer>().enabled = true;
                    }
                    break;
            }
        }
    }
}
