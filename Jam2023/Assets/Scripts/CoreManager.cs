using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : MonoBehaviour
{
    public FlashlightSystem playerToggle = null;
    public float darkBatteryStart;
    public float darkBatteryCurrent;
    public float batteryConsumption;
    public float batteryRecharge;

    // Start is called before the first frame update
    void Start()
    {
        playerToggle = FindObjectOfType<FlashlightSystem>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
