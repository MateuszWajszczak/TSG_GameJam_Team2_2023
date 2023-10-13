using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private Transform pickUpOriginPoint;
    [SerializeField] private float pickupAngle;
    [SerializeField] private float pickupRange;
    [SerializeField] private CoreManager myManager;
    public bool pickUpEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (pickUpEnabled)
        {
            PerformConicalRaycastPickUp();
        }
    }

    public void PerformConicalRaycastPickUp()
    {
        {
            Vector3 raycastOrigin = pickUpOriginPoint.transform.position;

            for (float angle = -pickupAngle / 2; angle <= pickupAngle / 2; angle += 1f) // Adjust the step size for the desired cone granularity
            {
                Vector3 raycastDirection = Quaternion.Euler(0, angle, 0) * pickUpOriginPoint.transform.forward;

                float raycastDistance = pickupRange;

                RaycastHit hit;
                if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance))
                {
                    PickUpObject otherObject = hit.collider.GetComponent<PickUpObject>();

                    if (otherObject != null)
                    {
                        // The "PickUpObject" component exists on the object, so you can access its public members
                        otherObject.currentPickUpTime = otherObject.maxPickUpTime;
                    }
                }
            }
        }
    }
}
