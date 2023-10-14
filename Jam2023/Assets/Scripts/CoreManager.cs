using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PickUpObject;

public class CoreManager : MonoBehaviour
{
    public FlashlightSystem playerToggle = null;
    public bool flashlightCollected = false;
    public float darkBatteryStart;
    public float darkBatteryCurrent;
    public float batteryConsumption;
    public float batteryRecharge;

    public PickUpObject currentObject = null;

    public int playerScore = 0;

    //UI Elements
    [SerializeField] private TextMeshProUGUI timertextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI objecttextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI countdowntextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI scoretextMeshProUGUI;
    [SerializeField] private GameObject countdowntImage;

    //Keys and Collectibles
    public Dictionary<int, bool> keyStatus = new Dictionary<int, bool>();
    public Dictionary<int, bool> collectibleStatus = new Dictionary<int, bool>();

    //Challenge Rooms
    public ChallengeObject currentChallenge = null;
    public float countdownRemaining = 0f;
    private float startTime;
    private float currentTime;
    public float remainingTime;

    //Player and transforms, set manually
    public GameObject myPlayerObject;
    public GameObject myPlayerFlashlight;
    public Transform hubStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerToggle = FindObjectOfType<FlashlightSystem>();
        UpdatePickUpObjectText();
        darkBatteryCurrent = darkBatteryStart;
        playerScore = 0;

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
        UpdateScoreText();

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

                case ObjectFunctions.Button:
                    objecttextMeshProUGUI.text = "Press:" + " " + currentObject.pickUpObjectName + " [E]";
                    break;

                case ObjectFunctions.Collectible:
                    objecttextMeshProUGUI.text = "Collect:" + " " + currentObject.pickUpObjectName + " [E]";
                    break;

                case ObjectFunctions.MazePortal:
                    objecttextMeshProUGUI.text = "Use the portal? [E]";
                    break;

                case ObjectFunctions.Flashlight:
                    objecttextMeshProUGUI.text = "Collect:" + " " + currentObject.pickUpObjectName + " [E]";
                    break;
                case ObjectFunctions.FinalDoor:
                    int keys = 0;
                    if (keyStatus.ContainsKey(1) && keyStatus[1] == true)
                    {
                        keys += 1;
                    }
                    if (keyStatus.ContainsKey(2) && keyStatus[2] == true)
                    {
                        keys += 1;
                    }
                    if (keyStatus.ContainsKey(3) && keyStatus[3] == true)
                    {
                        keys += 1;
                    }
                    if (keyStatus.ContainsKey(4) && keyStatus[4] == true)
                    {
                        keys += 1;
                    }
                    objecttextMeshProUGUI.text = "You have: " + keys.ToString() + " keys out of 4." + " [E]";
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

    public void UpdateScoreText()
    {
        scoretextMeshProUGUI.text = playerScore.ToString();
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

    public void TeleportPlayerMazePortal(Transform newPosition)
    {
        myPlayerObject.SetActive(false);
        myPlayerObject.transform.position = newPosition.position;
        myPlayerObject.SetActive(true);
    }

    public void CountdownTime()
    {
        currentTime = Time.time;
        remainingTime = countdownRemaining - (currentTime - startTime);
        int seconds = Mathf.FloorToInt(remainingTime);
        countdowntextMeshProUGUI.text = seconds.ToString();

        if (seconds <= 0 )
        {
            ChallengeTimeout();
        }
    }

    public void ChallengeTimeout()
    {
        currentChallenge = null;
        TeleportPlayerToHub();
    }

    public void KillPlayerInChallenge()
    {
        if (currentChallenge != null)
        {
            TeleportPlayerToChallenge();
        }
        else
        {
            TeleportPlayerToHub();
        }
    }

    public void EnableFlashlight()
    {
        flashlightCollected = true;
        myPlayerFlashlight.SetActive(true);
    }

    public void winGame()
    {
        Debug.Log("You win!");
    }
}
