using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include the TextMeshPro namespace


public class pouring : MonoBehaviour
{
    //shakerFill_execute script2Instance;

    public GameObject UIsayac;

    public Image filledImage;       // UI codes
    private float fillSpeed = 0.2f; // Speed of filling  
    public GameObject JiggerImage;

    public Behaviour drag;
    public float oran;
    private Quaternion targetRotation;
    private Quaternion initialRotation;


    private float resetSpeed = 0.2f; 
    private float rotationSpeed = 4.0f;


    public ParticleSystem particleSystem; // partikül sistemine eriþim
    public GameObject cubuk;

    void Start()
    {
        initialRotation = transform.rotation;
        particleSystem.Stop();
    }

    void OnCollisionEnter(Collision collision)                                          // Çarpýþma
    {
        if (enabled)
        {
            if (collision.gameObject.name == "colliderSpace_1")
            {
                transform.rotation = initialRotation;
                cubuk.SetActive(false);
                particleSystem.Stop();
                particleSystem.gameObject.SetActive(false);    // CHANGED
            }
        }
    }

    void OnMouseDown()
    {
        if (enabled)
        {
            if (gameObject.CompareTag("raf"))
            {
                initialRotation = transform.rotation;
            }
        }
    }

    void OnMouseDrag()
    {
        if (enabled)
        {
            if (drag.enabled == false)
            {
                oran = transform.position.y * 10;
                if (oran > 40)
                {
                    if (gameObject.tag == "mix")
                        oran = oran * 1.5f;
                    else
                        oran = oran * 2.5f;
                }




                targetRotation = Quaternion.Euler(-90 + oran, 90, -90);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                particleSystem.transform.rotation = Quaternion.Euler(90, 0, 0);
                cubuk.transform.rotation = initialRotation;
                if (gameObject.tag != "mix" && oran >= 110)
                {
                   

                    particleSystem.gameObject.SetActive(true);    // CHANGED
                    particleSystem.Play();
                    cubuk.SetActive(true);

                }
                else if (oran >= 65)
                {
                    particleSystem.gameObject.SetActive(true);    // CHANGED
                    particleSystem.Play();
                    cubuk.SetActive(true);

                }
                else
                {
                    particleSystem.gameObject.SetActive(false);    // CHANGED
                    particleSystem.Stop();
                    cubuk.SetActive(false);
                    CanvaCloser();
                }
            }
        }
    }
    void OnMouseUp()
    {
        if (enabled)
        {
            if (drag.enabled == false)
            {
                CanvaCloser();
                particleSystem.gameObject.SetActive(false);    // CHANGED
                particleSystem.Stop();
                cubuk.SetActive(false);
                StartCoroutine(ResetRotationSmoothly());

            }
        }
    }

    IEnumerator ResetRotationSmoothly()
    {
        if (enabled)
        {
            float elapsedTime = 0f;
            Quaternion currentRotation = transform.rotation;

            while (elapsedTime < resetSpeed)
            {
                transform.rotation = Quaternion.Lerp(currentRotation, initialRotation, (elapsedTime / resetSpeed));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = initialRotation;
        }
        
    }

    public void CanvaCloser()
    {
        UIsayac.SetActive(false);
        filledImage.fillAmount = 0;
        JiggerImage.SetActive(false);
    }



}



