using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tezgah : MonoBehaviour
{
    shaker_animation shakerScript;


    private Vector3 location1_position = new Vector3(1.20899999f, 2.40700006f, 2.06399989f);

    private Vector3 location2_position = new Vector3(0.384977341f, 2.41700006f, 2.12287927f);

    private Vector3 location3_position = new Vector3(-0.411000013f, 2.41700006f, 2.12287927f);

    private Vector3 location4_position = new Vector3(-1.19889998f, 2.41400003f, 2.16050005f);


    private Vector3 fark;

    public Behaviour dragScript;
    // public Behaviour outliner;

    public GameObject liquid;

    public GameObject emptyTheDrink;

    private Vector3 initialPosition;


    public float transitionDuration = 0.8f;
    public float elapsedTime = 0f;

 
    private float rotationSpeed = 400;
    private float targetRotation = 90f;

    byte sayac; // singletona bir eri�im �ekli

    public GameObject collider1;

    public Transform bosParent;     // popup tan se�ti�im objeleri ailesinden ay�rmak i�in yeni bos bir aile olu�turuyom
    public Transform popG;          // tekrar ailesinin yan�na d�nmesi i�in ger�ek parent
    private bool aile = true;


    private Vector3 initialPosition2; // tezgahta uzakl��� korumak i�in geldi�indeki pozisyonunu al�yorum

    public void yerlestir(string name)
    {
        if (gameObject.CompareTag("bardak"))
        {
            glassTrigger gtn = GetComponent<glassTrigger>();
            gtn.kopruFonksiyonu();
            ChangeParentForGlasses(transform, bosParent,popG,aile);
            boyutAyarlama(true);
        }

        if (gameObject.CompareTag("soft"))
            boyutAyarlama(true);

        if(name == "clubSoda")
            transform.Rotate(0f, -90f, 0f);

        StartCoroutine(RotateOverTime());

        if (singleton_db.instance.location1==false)
        {
            //yerlestir;
            StartCoroutine(konumlandir(location1_position));
            singleton_db.instance.location1 = true;
            sayac= 1;
        }
        else if(singleton_db.instance.location2 ==false)
        {
            StartCoroutine(konumlandir(location2_position));
            //yerlesitr;
            singleton_db.instance.location2 = true;
            sayac= 2;
        }
        else if(singleton_db.instance.location3 ==false)
        {
            //yerlestir;
            StartCoroutine(konumlandir(location3_position));
            singleton_db.instance.location3 = true;
            sayac = 3;
            
        }
        else if(singleton_db.instance.location4 ==false)
        {
            //yerlestir;
            StartCoroutine(konumlandir(location4_position));
            singleton_db.instance.location4 = true;
            sayac = 4;
        }
    }       // objeleri kars�lama ve yerlestirme fonk
    
    
    
    IEnumerator RotateOverTime()                // 90 derece d�nd�r
    {
        float currentRotation = 0f;

        if (!gameObject.CompareTag("soft") && !gameObject.CompareTag("istasyon") && !gameObject.CompareTag("bardak") && !gameObject.CompareTag("shaker") && !gameObject.CompareTag("mix"))
        {
            while (currentRotation < targetRotation)
            {
                float deltaRotation = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.forward, deltaRotation);
                currentRotation += deltaRotation;

                yield return null; // Wait for the next frame
            }
        }
    }
    
    
    
    void OnCollisionEnter(Collision collision)                                          // �arp��ma
    {
        if (dragScript.enabled == false)
        {

            if (collision.gameObject.name == "colliderSpace_1")
            {
                    geriDonus(sayac);
            }

            if (collision.gameObject.name == "colliderSpace_4_popup")
            {
                if(gameObject.CompareTag("shaker") || gameObject.CompareTag("mix") || gameObject.CompareTag("bardak"))
                {
                    if (gameObject.CompareTag("shaker") && singleton_db.instance.readyToShake)
                    {
                        Debug.Log("shakerin i�i dolu");
                        emptyTheDrink_no();
                    }
                    else
                    {
                        Debug.Log("shaker bo�alt�l�yor");
                        emptyTheDrink_yes();
                    }

                    if (gameObject.CompareTag("mix") && singleton_db.instance.readyToMix)
                    {
                        Debug.Log("mixerin i�i dolu");
                        emptyTheDrink_no();
                    }
                    else
                    {
                        Debug.Log("mix bo�alt�l�yor");
                        emptyTheDrink_yes();
                    }

                    if (gameObject.CompareTag("bardak"))
                    {
                        Transform liquidTransform = collision.gameObject.transform.Find("liquid");

                        if (liquidTransform != null && liquidTransform.gameObject.activeSelf)      // barda��n i�inde liquid var m�  bak�yorum
                        {
                            Debug.Log("bardak dolu");
                            emptyTheDrink_no();
                        }
                        else
                        {
                            Debug.Log("bardak bo�alt�l�yor");
                            emptyTheDrink_yes();
                        }

                    }
                }
            }
        }
    }



    void boyutAyarlama(bool alma_koyma)     // softlar�n boyutunu ge�ici b�y�ten fonk
    {
        Vector3 currentScale = transform.localScale;

        if (alma_koyma == true)
        {
            if (gameObject.CompareTag("bardak"))
            {
                if (gameObject.name == "shotGlass")
                    transform.localScale = currentScale * 1.4f;
                if (gameObject.name == "shortOndrck")
                    transform.localScale = currentScale * 1.2f;
                if (gameObject.name == "longOndrck")
                    transform.localScale = currentScale * 1.3f;
                if (gameObject.name == "beerGlass")
                    transform.localScale = currentScale * 1.2f;
                if (gameObject.name == "bavyera")
                    transform.localScale = currentScale * 1.2f;
                if (gameObject.name == "cocktailGlass")
                    transform.localScale = currentScale * 1.2f;

            }
            else 
                transform.localScale = currentScale * 1.5f;
        }
        else
        {
            transform.localScale = currentScale / 1.5f;

        }


    }
    
    
    
    void geriDonus(byte swc)
    {

        switch (swc)
        {
            case 1:
                singleton_db.instance.location1 = false;
                break;
            case 2:
                singleton_db.instance.location2 = false;
                break;
            case 3:
                singleton_db.instance.location3 = false;
                break;
            case 4:
                singleton_db.instance.location4 = false;
                break;
            default:
                break;
        }
        if (!gameObject.CompareTag("soft") && !gameObject.CompareTag("istasyon") && !gameObject.CompareTag("bardak") && !gameObject.CompareTag("shaker") && !gameObject.CompareTag("mix"))
        {
            transform.Rotate(0f, 0f, -90f);

        }

        if(name == "clubSoda")
            transform.Rotate(0f, 90f, 0f);

        if (gameObject.CompareTag("soft"))
            boyutAyarlama(false);
         if(gameObject.CompareTag("bardak"))
        {
            Destroy(gameObject);
            collider1.SetActive(false);
        }

         if(gameObject.CompareTag("mix"))
        {
            singleton_db.instance.readyToMix = false;
            liquid.transform.localScale = new Vector3(0.950693727f, 0.950693846f, 0.0590403937f);
            liquid.SetActive(false);
        }
          if(gameObject.CompareTag("shaker"))
        {
            singleton_db.instance.readyToShake = false;

        }


        dragScript.enabled = true;
        drag dragNesnesi = GetComponent<drag>();  //di�er scripte eri�mek i�in izin
        dragNesnesi.toTezgah = false;
        dragNesnesi.ReturnToInitialPosition();

        if (!gameObject.CompareTag("bardak"))   // GEcici bir if, ��nk� shakerde hen�z pour scripti yok
        {
            pouring pour = GetComponent<pouring>();
            pour.CanvaCloser();
            if (gameObject.CompareTag("mix") || gameObject.CompareTag("shaker"))
            {
                mixOrShakeDone mosd = GetComponent<mixOrShakeDone>();
                mosd.exitMosd();
            }
            if(pour.cubuk!= null)
            {
                pour.cubuk.SetActive(false);
            }
        }


    }


    IEnumerator konumlandir(Vector3 hedef)
    {
        float elapsedTime = 0f;
        float duration = 0.2f; // Geri d�nme s�resi

        Vector3 currentPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(currentPosition, hedef, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Tam olarak ba�lang�� pozisyonuna ayarla (olayl� hatalar� d�zeltmek i�in)
        transform.position = hedef;
        dragScript.enabled = false;                     // drag scriptini deaktif ettim
        initialPosition = transform.position;           // yerle�tirdikten sonraki pozisyonunu ana pozsiyonu olarak kaydettim

    }

    
    void OnMouseDown()
    {
        singleton_db.instance.selectedOne = gameObject;     // SET THE SELECTED OBJECT 
        singleton_db.instance.fonk(sayac);


        if (dragScript.enabled == false)
        {
            collider1.SetActive(true);
            
            initialPosition2 = transform.position;

            if (gameObject.CompareTag("soda"))
            {
                transform.Rotate(0f, -90f, 0f);
            }

            fark = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

            // UI SETT�NGS
            if (singleton_db.instance.isThereAnyObjectStanding != true)
            {
                if (gameObject.CompareTag("bardak"))
                {
                    singleton_db.instance.canva_bardak.SetActive(true);
                    singleton_db.instance.canva_mix.SetActive(false);
                    singleton_db.instance.canva_shaker.SetActive(false);
                    singleton_db.instance.canva_other.SetActive(true);
                }
                if (gameObject.CompareTag("shaker"))
                {
                    singleton_db.instance.canva_bardak.SetActive(false);
                    singleton_db.instance.canva_mix.SetActive(false);
                    singleton_db.instance.canva_shaker.SetActive(true);
                    singleton_db.instance.canva_other.SetActive(true);
                }
                if (gameObject.CompareTag("mix"))
                {
                    singleton_db.instance.canva_bardak.SetActive(false);
                    singleton_db.instance.canva_mix.SetActive(true);
                    singleton_db.instance.canva_shaker.SetActive(false);
                    singleton_db.instance.canva_other.SetActive(true);
                }
            }
        }
    }
    void OnMouseDrag()
    {

        if (dragScript.enabled == false)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - fark);
            //
            transform.position = new Vector3(transform.position.x, transform.position.y, initialPosition2.z);
        }
        float distance = Vector3.Distance(transform.position, initialPosition);
        if (distance >= 0.5f)
        {
            singleton_db.instance.canva_other.SetActive(false);
            singleton_db.instance.delete_outline();


        }
    }
    void OnMouseUp()
    {
        if (dragScript.enabled == false)
        {

            collider1.SetActive(false);
            StartCoroutine(ReturnToInitialPosition());
           
        }
    }


    IEnumerator ReturnToInitialPosition()       // tezgahta i�lem yaparken tekrar tezgaha koymak i�in gerekli fonk
    {
        float elapsedTime = 0f;
        float duration = 0.2f; // Geri d�nme s�resi

        Vector3 currentPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(currentPosition, initialPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Tam olarak ba�lang�� pozisyonuna ayarla (olayl� hatalar� d�zeltmek i�in)
        transform.position = initialPosition;
    }



    void ChangeParentForGlasses(Transform child, Transform bosParent, Transform popG, bool aile)
    {

        if (aile == true)
        {
            // Check if both child and newParent are not null
            if (child != null && bosParent != null)
            {
                // Change the parent of the child
                child.SetParent(bosParent);
            }
            else
            {
                Debug.LogError("Child or newParent is null.");
            }
        }
        else
        {
            // Check if both child and newParent are not null
            if (child != null && popG != null)
            {
                // Change the parent of the child
                child.SetParent(popG);
            }
            else
            {
                Debug.LogError("Child or newParent is null.");
            }
        }
    }

    public void emptyTheDrink_yes()
    {
        Debug.Log("�LANSf�land�flkjNS�KDJG�");
        if (gameObject.CompareTag("shaker"))
        {
            shakerScript = FindObjectOfType<shaker_animation>();
            if (shakerScript != null)
            {
                shakerScript.closeShaker();

            }
        }
        geriDonus(sayac);


        emptyTheDrink.SetActive(false);



    }

    public void emptyTheDrink_no()
    {
        emptyTheDrink.SetActive(false);
    }




}

