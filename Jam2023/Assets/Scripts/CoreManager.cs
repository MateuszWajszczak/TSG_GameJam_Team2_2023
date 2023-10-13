using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PickUpObject;

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
    [SerializeField] private TextMeshProUGUI countdowntextMeshProUGUI;
    [SerializeField] private GameObject countdowntImage;

    //Keys and Collectibles
    private Dictionary<int, bool> keyStatus = new Dictionary<int, bool>();
    private Dictionary<int, bool> collectibleStatus = new Dictionary<int, bool>();

    //Challenge Rooms
    public ChallengeObject currentChallenge = null;
    public float countdownRemaining = 0f;
    private float startTime;
    private float currentTime;

    //Player and transforms, set manually
    public GameObject myPlayerObject;
    public Transform hubStartPosition;

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

        if (currentChallenge != null)
        {
            CountdownTime();
        }
        else
        {
            countdowntextMeshProUGUI.text = null;
            countdowntImage.SetActive(false);
        }

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
            switch (currentObject.myObjectFunction)
            {
                case ObjectFunctions.Key:
                    objecttextMeshProUGUI.text = "Collect:" + " " + currentObject.pickUpObjectName + " [E]";
                    break;

                case ObjectFunctions.Challenge:
                    objecttextMeshProUGUI.text = "Enter:" + " " + currentObject.pickUpObjectName + " [E]";
                    break;

                case ObjectFunctions.Collectible:
                    objecttextMeshProUGUI.text = "Collect:" + " " + currentObject.pickUpObjectName + " [E]";
                    break;
            }
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
        countdowntImage.SetActive(true);
        currentChallenge = newChallenge;
        countdownRemaining = newChallenge.challengeTimer;
        startTime = Time.time;
        TeleportPlayerToChallenge();
    }

    public void TeleportPlayerToChallenge()
    {
        myPlayerObject.SetActive(false);
        myPlayerObject.transform.position = currentChallenge.challengeStartPosition;
        myPlayerObject.SetActive(true);
    }

    public void TeleportPlayerToHub()
    {
        myPlayerObject.SetActive(false);
        myPlayerObject.transform.position = hubStartPosition.position;
        myPlayerObject.SetActive(true);
    }

    public void CountdownTime()
    {
        currentTime = Time.time;
        float remainingTime = countdownRemaining - (currentTime - startTime);
        int seconds = Mathf.FloorToInt(remainingTime);
        countdowntextMeshProUGUI.text = seconds.ToString();

        if (seconds <= 0 )
        {
            ChalengeTimeout();
        }
    }

    public void ChalengeTimeout()
    {
        currentChallenge = null;
        TeleportPlayerToHub();
    }
}
