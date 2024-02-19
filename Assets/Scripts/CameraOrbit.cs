using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform playerTransform;
    public float orbitRadius = 5f;
    public float orbitSpeed = 1f;

    void Update()
    {
        // Calculate orbit position
        Vector3 orbitPosition = playerTransform.position + new Vector3(Mathf.Sin(Time.time * orbitSpeed) * orbitRadius, orbitRadius, Mathf.Cos(Time.time * orbitSpeed) * orbitRadius);

        // Set camera position
        transform.position = orbitPosition;

        // Look at the player
        transform.LookAt(playerTransform.position);
    }
}
