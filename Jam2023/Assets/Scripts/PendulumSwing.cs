using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    public float swingSpeed = 45.0f; // Speed of the swing in degrees per second
    public float maxSwingAngle = 45.0f; // Maximum swing angle in degrees
    public float startAngle = 0.0f; // Initial angle
    public bool startSwinging = true; // Whether the pendulum starts swinging automatically
    public DarkToggle myToggle;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation;

        initialRotation = transform.rotation; 
    }

    private void Update()
    {
        startSwinging = myToggle.movementFrozen;

        if (myToggle.movementFrozen == false)
        {
            targetRotation = transform.rotation;
        }

        if (startSwinging)
        {
            targetRotation = initialRotation * Quaternion.Euler(0, 0, startAngle + maxSwingAngle * Mathf.Sin(swingSpeed * Time.time));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * swingSpeed);
        }
    }

}
