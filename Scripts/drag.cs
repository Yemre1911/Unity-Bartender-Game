using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    shaker_animation shakerScript;

    private Vector3 fark;
    private string objenin_ismi;

    public Vector3 initialPosition;         //ilk pozisyon
    private Quaternion initialRotation;     // ilk rotasyon

    private Vector3 initialPositionForGlasses;
    public bool toTezgah = false;

    public GameObject colliderAlani;    //raf için
    public GameObject colliderAlani2; // popup için

    public bool tezgah_raf_parametresi = false;


    void Start()
    {
        // Baþlangýç pozisyonunu kaydet
        initialPosition = transform.position;
        initialRotation = transform.rotation;

    }



    void OnCollisionEnter(Collision collision)                                          // Çarpýþma
    {
        if (enabled)
        {
            if (gameObject.CompareTag("raf"))
            {
                if (collision.gameObject.name == "colliderSpace_1")
                {
                    if (singleton_db.instance.location1 == true && singleton_db.instance.location2 == true && singleton_db.instance.location3 == true && singleton_db.instance.location4 == true)
                    {
                        toTezgah = false;
                    }
                    else
                        toTezgah = true;
                }
            }
            if (gameObject.CompareTag("soft"))
            {
                if (collision.gameObject.name == "colliderSpace_2_soft")
                {
                    if (singleton_db.instance.location1 == true && singleton_db.instance.location2 == true && singleton_db.instance.location3 == true && singleton_db.instance.location4 == true)
                    {
                        toTezgah = false;
                    }
                    else
                        toTezgah = true;
                }
            }
            if (gameObject.CompareTag("istasyon"))
            {
                if (collision.gameObject.name == "colliderSpace_3_istasyon")
                {
                    if (singleton_db.instance.location1 == true && singleton_db.instance.location2 == true && singleton_db.instance.location3 == true && singleton_db.instance.location4 == true)
                    {
                        toTezgah = false;
                    }
                    else
                        toTezgah = true;
                }
              
            }
            if (gameObject.CompareTag("bardak") || gameObject.CompareTag("shaker") || gameObject.CompareTag("mix"))
            {
                if (collision.gameObject.name == "colliderSpace_4_popup")
                {
                    if (gameObject.CompareTag("shaker"))
                    {
                        shakerScript = FindObjectOfType<shaker_animation>();
                        if (shakerScript != null)
                        {
                            shakerScript.openShaker();
                        }
                    }

                    if (singleton_db.instance.location1 == true && singleton_db.instance.location2 == true && singleton_db.instance.location3 == true && singleton_db.instance.location4 == true)
                    {
                        toTezgah = false;
                    }
                    else
                    {
                        toTezgah = true;
                        Debug.Log("aosfnjoasd");
                    }
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (enabled)
        {
            singleton_db.instance.selectedOne = gameObject;     // SET THE SELECTED OBJECT 


            if (gameObject.CompareTag("raf") || gameObject.CompareTag("shaker") || gameObject.CompareTag("mix"))
            {
                colliderAlani.SetActive(true);
            }
            // carpýsma alaný etkinleþtirildi
            if (gameObject.CompareTag("bardak"))
            {
                initialPositionForGlasses = transform.position;   // bardaklarýn ilk sabit yeri deðiþken olduðu için týkladýktan sonraki yerini baz alýyoruz 
                colliderAlani2.SetActive(true);
            }

                if (gameObject.CompareTag("soda"))
            {
                transform.Rotate(0f, -90f, 0f);
            }

            fark = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            
        }
    }
    void OnMouseDrag()
    {
        if (enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - fark);
        }
    }
    void OnMouseUp()
    {
        if (enabled)
        {
            if (toTezgah == false)
            {
               
                StartCoroutine(ReturnToInitialPosition());

                if (gameObject.CompareTag("soda"))
                {
                    transform.Rotate(0f, +90f, 0f);
                }
            }
            else
            {
                tezgah tezgah_nesnesi = GetComponent<tezgah>();  //diðer scripte eriþmek için izin
                objenin_ismi = gameObject.name;
                tezgah_nesnesi.yerlestir(objenin_ismi);
                tezgah_nesnesi.collider1 = colliderAlani;

            }
            
            colliderAlani.SetActive(false);     // carpýsma alani devre dýþý
            colliderAlani2.SetActive(false);

        }
    }


    public IEnumerator ReturnToInitialPosition()    //objeleri ilk yerlerine geri döndür        
    {
        float elapsedTime = 0f;
        float duration = 0.2f; // Geri dönme süresi

        Vector3 currentPosition = transform.position;
        Vector3 positionHolder; // bu fonksiyonda kullanýlacak hededf pozisyonu alýr; bardaksa farklý þiþeyse farklý
      

        if (gameObject.CompareTag("bardak") && tezgah_raf_parametresi == false)
        {
            positionHolder = initialPositionForGlasses;
        }

        else
        {
            positionHolder = initialPosition;
        }

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(currentPosition, positionHolder, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = initialRotation;

        transform.position = positionHolder;
        tezgah_raf_parametresi = false;
    }

}
