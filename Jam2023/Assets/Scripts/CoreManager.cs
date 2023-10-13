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

    // Start is called before the first frame update
    void Start()
    {
        playerToggle = FindObjectOfType<FlashlightSystem>();
        UpdatePickUpObjectText();
        darkBatteryCurrent = darkBatteryStart;
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
}
