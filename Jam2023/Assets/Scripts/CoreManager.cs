using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoreManager : MonoBehaviour
{
    public FlashlightSystem playerToggle = null;
    public float darkBatteryStart;
    public float darkBatteryCurrent;
    public float batteryConsumption;
    public float batteryRecharge;

    public PickUpObject currentObject = null;

    //UI Elements
    [SerializeField] private TextMeshProUGUI timertextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI objecttextMeshProUGUI;

    //Keys and Collectibles
    private Dictionary<int, bool> keyStatus = new Dictionary<int, bool>();
    private Dictionary<int, bool> collectibleStatus = new Dictionary<int, bool>();

    //Challenge Rooms
    public ChallengeObject currentChallenge = null;

    //Player, set manually
    public GameObject myPlayerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerToggle = FindObjectOfType<FlashlightSystem>();
        UpdatePickUpObjectText();
        darkBatteryCurrent = darkBatteryStart;

        //Keys
        keyStatus.Add(1, false); 
        keyStatus.Add(2, false);
        keyStatus.Add(3, false);
        keyStatus.Add(4, false);

        //Collectibles
        collectibleStatus.Add(1, false);
        collectibleStatus.Add(2, false);
        collectibleStatus.Add(3, false);
        collectibleStatus.Add(4, false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBatteryText();

        if (playerToggle.isDark)
        {
            darkBatteryCurrent -= batteryConsumption * Time.deltaTime;
            if (darkBatteryCurrent <= 0)
            {
                playerToggle.ToggleFlashlightMode();
            }
        }
        else if (playerToggle.isDark == false)
        {
            if (darkBatteryCurrent <= darkBatteryStart)
            {
                darkBatteryCurrent += batteryRecharge * Time.deltaTime;
            }
        }
    }

    public void UpdatePickUpObjectText()
    {
        if (currentObject != null)
        {
            objecttextMeshProUGUI.text = "Collect:" + " " + currentObject.pickUpObjectName + " [E]";
        }
        else
        {
            objecttextMeshProUGUI.text = null;
        }
    }

    public void UpdateBatteryText()
    {
        int percentage = Mathf.RoundToInt((darkBatteryCurrent / darkBatteryStart) * 100);
        timertextMeshProUGUI.text = percentage.ToString() + "%";
    }

    public void CollectKey(int keyID)
    {
        if (keyStatus.ContainsKey(keyID))
        {
            keyStatus[keyID] = true; // Set the status to collected
        }
    }

    public void CollectColectible(int collectibleID)
    {
        if (collectibleStatus.ContainsKey(collectibleID))
        {
            collectibleStatus[collectibleID] = true; // Set the status to collected
        }
    }

    public Dictionary<int, bool> GetKeyStatusDictionary()
    {
        return keyStatus;
    }

    public Dictionary<int, bool> GetCollectibleStatusDictionary()
    {
        return collectibleStatus;
    }

    public void BeginChallenge(ChallengeObject newChallenge)
    {
        currentChallenge = newChallenge;
        TeleportPlayerToChallenge();
    }

    public void TeleportPlayerToChallenge()
    {
        myPlayerObject.SetActive(false);
        Debug.Log("Teleporting!");
        myPlayerObject.transform.position = currentChallenge.challengeStartPosition;
        myPlayerObject.SetActive(true);
    }
}
