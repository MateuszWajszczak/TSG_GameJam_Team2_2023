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
    public TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        playerToggle = FindObjectOfType<FlashlightSystem>();
        darkBatteryCurrent = darkBatteryStart;
    }

    // Update is called once per frame
    void Update()
    {
        int percentage = Mathf.RoundToInt((darkBatteryCurrent / darkBatteryStart) * 100);
        textMeshProUGUI.text = percentage.ToString() + "%";


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
}
