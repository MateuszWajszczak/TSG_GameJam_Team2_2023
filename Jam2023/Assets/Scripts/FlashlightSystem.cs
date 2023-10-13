using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    public GameObject brightLight;
    public GameObject darkLight;

    public bool isDark;
    public float coneAngle = 45f;
    public float darkLightAddValue = 0.5f;

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
            PerformConicalRaycast();
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

    private void PerformConicalRaycast()
    {
        {
            Vector3 raycastOrigin = darkLight.transform.position;

            for (float angle = -coneAngle / 2; angle <= coneAngle / 2; angle += 1f) // Adjust the step size for the desired cone granularity
            {
                Vector3 raycastDirection = Quaternion.Euler(0, angle, 0) * darkLight.transform.forward;

                float raycastDistance = darkLight.GetComponent<Light>().range;

                RaycastHit hit;
                if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance))
                {
                    DarkToggle darkToggleComponent = hit.collider.GetComponent<DarkToggle>();

                    if (darkToggleComponent != null)
                    {
                        // The "darkToggle" component exists on the object, so you can access its public members
                        darkToggleComponent.darkLightTime = darkLightAddValue;
                    }
                }
            }
        }
    }
}
