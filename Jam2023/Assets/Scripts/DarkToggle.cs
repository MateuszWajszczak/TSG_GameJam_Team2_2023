using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DarkToggle;

public class DarkToggle : MonoBehaviour
{
    public enum DarkFunctions
    {
        Freeze,
        Show,
        // Add more values as needed
    }

    public float darkLightTime = 0f;
    public bool darkMode = false;
    public bool movementFrozen = false;

    public DarkFunctions myFunction = DarkFunctions.Show;

    // Start is called before the first frame update
    void Start()
    {
        darkMode = false;
        DarkModeOnDisabled();
    }

    // Update is called once per frame
    void Update()
    {
        if (darkLightTime > 0)
        {
            darkLightTime -= 1 * Time.deltaTime;
            if ((darkMode == false) && (darkLightTime > 0)) 
            {
                darkMode = true;
                DarkModeOnEnabled();
            }
        }
        else
        {
            if (darkMode == true)
            {
                DarkModeOnDisabled();
            }
            darkMode = false;
        }
    }

    public void DarkModeOnDisabled()
    {
        switch (myFunction)
        {
            case DarkFunctions.Freeze:
                movementFrozen = true;
                break;

            case DarkFunctions.Show:
                GetComponent<Renderer>().enabled = false;
                break;

                // Add more cases for additional enum values
        }
    }

    public void DarkModeOnEnabled()
    {
        switch (myFunction)
        {
            case DarkFunctions.Freeze:
                movementFrozen = false;
                break;

            case DarkFunctions.Show:
                GetComponent<Renderer>().enabled = true;
                break;

                // Add more cases for additional enum values
        }
    }
}
