using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour
{
    private GameObject target; // Reference to your target GameObject
    public float zoomSpeed = 1.0f; // Speed of zooming
    public float minFOV = 20f; // Minimum field of view
    public float maxFOV = 60f; // Maximum field of view

    public Camera cam;

    public void ZoomButton()
    {
        Debug.Log("Button Presses");
        target = singleton_db.instance.selectedOne;
        if (target == null)
            return;

        // Calculate the distance between the camera and the target
        float distance = Vector3.Distance(transform.position, target.transform.position);

        // Calculate the desired field of view based on distance
        float desiredFOV = Mathf.Clamp(distance * zoomSpeed, minFOV, maxFOV);

        // Smoothly interpolate between the current field of view and the desired field of view
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, desiredFOV, Time.deltaTime * zoomSpeed);

        // Alternatively, you can use Vector3.Lerp to move the camera towards the target while zooming
        // transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * zoomSpeed);
    }
}
