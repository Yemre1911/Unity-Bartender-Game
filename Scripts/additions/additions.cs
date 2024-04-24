using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class additions : MonoBehaviour
{
    public GameObject textImage;
    public Text error;

    public GameObject AddTo;

    private bool swc = false;

    void Start()
    {
        if (imageToShake != null)
        {
            // Store the original position of the image
            originalPosition = imageToShake.localPosition;
        }
    }

    void OnMouseDown()
    {
        if (swc == false)
            swc = true;
        else
            swc = false;

        if (AddTo != singleton_db.instance.AddToThisObject || AddTo == null)
            swc = true;

        transform.localScale = transform.localScale * 1.2f;
        AddTo = singleton_db.instance.AddToThisObject;
        ActivateChild(gameObject.name, swc);


    }
    void OnMouseUp()
    {
        transform.localScale = transform.localScale / 1.2f;

    }

    void ActivateChild(string childName, bool swc)
    {
        if (AddTo != null)
        {
            Transform child = AddTo.transform.Find(childName);
            if (child != null)
            {
                child.gameObject.SetActive(swc);
            }
            else
            {
                // Debug.Log("No child with name " + childName + " found!");
                textImage.SetActive(true);
                error.text = "You can't add "+gameObject.name+" to this !!";
                StartCoroutine(WaitAndExecute());
                StartCoroutine(Shake());
            }
        }

    }

    IEnumerator WaitAndExecute()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);
        textImage.SetActive(false);
        // Code to execute after the wait
    }


    public RectTransform imageToShake;

    // Shake parameters
    private readonly float shakeDuration = 0.5f;
    private float shakeMagnitude = 5f;

    private Vector3 originalPosition;

    IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Apply the shake offset to the original position
            imageToShake.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Reset to original position
        imageToShake.localPosition = originalPosition;
    }


}