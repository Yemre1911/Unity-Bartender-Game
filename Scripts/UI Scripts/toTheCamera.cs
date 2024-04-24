using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toTheCamera : MonoBehaviour
{

    private Collider colliderComponent;
    private GameObject mainObject;
    public GameObject cp1;
    public GameObject cp2;

    public Transform parentObject; // Reference to the parent object
    public Transform parentObjectForButton;
    public GameObject cube;
    private Vector3 targetPosition;  // sahne
    private Vector3 targetPositionForCube;
    public float smoothSpeed = 0.5f; // The speed of the smooth movement
    private Vector3 initialPosition; // Initial position of the object
    private Vector3 initialPositionForCube;

    private Transform originalParent; // Store reference to the original parent
    public  Transform originalParentOfButton; // Store reference to the original parent

    private bool swc = true;


    public Button moveButton; // Reference to the UI Button


    void start()
    {
        moveButton.onClick.AddListener(MoveToTargetOnClick); // button dinlemede
    }

     public void MoveToTargetOnClick()
    {
        targetPosition = cp1.transform.position;
        targetPositionForCube = cp2.transform.position;
        

        if (swc == true)
        {
            mainObject = singleton_db.instance.selectedOne; // seçili olan objeyi aldým

            colliderComponent = mainObject.GetComponent<Collider>();


            originalParent = mainObject.transform.parent;
            initialPosition = mainObject.transform.position;
            initialPositionForCube = cube.transform.position;
            StartCoroutine(MoveToTarget());
            swc = false;
        }
        else
        {
            DisableCollidersRecursive(mainObject.transform,false); 
            moveButton.transform.SetParent(originalParentOfButton);
            mainObject.transform.SetParent(originalParent);
            StartCoroutine(MoveBack());
            swc = true;
        }
    }
 
    IEnumerator MoveToTarget()
    {
        Debug.Log("Initial Position: " + mainObject.transform.position);
        Debug.Log("Initial Position CUbe: " + cube.transform.position);

        float elapsedTime = 0f;
        Vector3 startingPos = mainObject.transform.position;
        Vector3 startingPos2 = cube.transform.position;

        while (elapsedTime < smoothSpeed)
        {
            mainObject.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / smoothSpeed));
            cube.transform.position = Vector3.Lerp(startingPos2, targetPositionForCube, (elapsedTime / smoothSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cube.transform.position = targetPositionForCube;
        mainObject.transform.position = targetPosition;

        mainObject.transform.SetParent(parentObject);
        moveButton.transform.SetParent(parentObjectForButton);


        DisableCollidersRecursive(singleton_db.instance.selectedOne.transform, true);
    }

    IEnumerator MoveBack()
    {
        Debug.Log(" MOVE BACK");

        float elapsedTime = 0f;
        Vector3 startingPos = mainObject.transform.position;
        Vector3 startingPos2 = cube.transform.position;

        while (elapsedTime < smoothSpeed)
        {
            mainObject.transform.position = Vector3.Lerp(startingPos, initialPosition, (elapsedTime / smoothSpeed));
            cube.transform.position = Vector3.Lerp(startingPos2, initialPositionForCube, (elapsedTime / smoothSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cube.transform.position = initialPositionForCube;
        mainObject.transform.position = initialPosition;
    }

    void DisableCollidersRecursive(Transform parent, bool swich)
    {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>(true);
        foreach (Collider collider in colliders)
        {
            collider.enabled = !swich;
        }
    }
}
