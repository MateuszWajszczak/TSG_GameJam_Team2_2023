using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    public GameObject brightLight;
    public GameObject darkLight;

    public bool isDark;

    // Start is called before the first frame update
    void Start()
    {
        brightLight.SetActive(true);
        darkLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDark)
        {
            if (!darkLight.activeSelf)
            {
                brightLight.SetActive(false);
                darkLight.SetActive(true);
            }
        }
        else
        {
            if (!brightLight.activeSelf)
            {
                brightLight.SetActive(true);
                darkLight.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlightMode();
        }
    }
    private void ToggleFlashlightMode()
    {
        isDark = !isDark;
    }
}
