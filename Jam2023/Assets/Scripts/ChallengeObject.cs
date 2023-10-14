using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeObject : MonoBehaviour
{
    public float challengeTimer = 60f;
    public Vector3 challengeStartPosition;

    private void Start()
    {
        challengeStartPosition = transform.position;
    }
}
