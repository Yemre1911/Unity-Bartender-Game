using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Include this for EventSystem

public class blocker_miniScript : MonoBehaviour
{
    public GameObject canva;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detects left mouse click
        {
            // Check if the click is on a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // The click is on a UI element, don't perform any further actions
                return;
            }

            // Perform a raycast to check for non-UI objects
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the raycast hit this specific game object
                if (hit.collider.gameObject == gameObject)
                {
                    // Perform actions if this game object was clicked
                    // ...
                }
            }
            else
            {
                // The click was in empty space, perform the desired action
                glassTrigger gtn = GetComponent<glassTrigger>();
                if (gtn != null)
                {
                    gtn.kopruFonksiyonu();
                }
                canva.SetActive(false);

            }
        }
    }
}
