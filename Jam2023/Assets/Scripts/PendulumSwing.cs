using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    public float swingSpeed = 45.0f; // Speed of the swing in degrees per second
    public float maxSwingAngle = 45.0f; // Maximum swing angle in degrees
    public float startAngle = 0.0f; // Initial angle
    public bool startSwinging = true; // Whether the pendulum starts swinging automatically
    public DarkToggle myToggle;

    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
        myToggle = GetComponent<DarkToggle>();  
    }

    private void Update()
    {
        startSwinging = myToggle.movementNotFrozen;

        if (startSwinging)
        {
            float angle = startAngle + maxSwingAngle * Mathf.Sin(swingSpeed * Time.time);
            transform.rotation = initialRotation * Quaternion.Euler(0, 0, angle);
        }
    }

}
