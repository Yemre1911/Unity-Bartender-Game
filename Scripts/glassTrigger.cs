using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class glassTrigger : MonoBehaviour
{

    // bardakSpawnOrDelete script2Instance;    // bardakSpawnOrDelete scriptine eri�mek i�in gerekli kod
   // shaker_animation shakerScript;
    // ----------------------------------------------------------------------------------------------------------

    public GameObject prefabInstance; // i�i yok edilecek parentin adresi

    public GameObject prefab; // t�m raf� spawnlay�p yok etmek i�in
    private GameObject spawnedObject; // spawn i�in                                         // Spawn bilgileri
    private Vector3 initialPosition_pf;        // ilk pozisyon
    private Quaternion initialRotation_pf;     // ilk rotasyon
    private Vector3 initialScale_pf;           // ilk boyut

    //---------------------------------------------------------------------------------------------------------

    public Transform gPop;  // raf objesinin transform bilgilerini buraya atad�m
    public Vector3 hedef = new Vector3(2.68449998f, 4.4000001f, 1.98819995f);
    private Vector3 initialPosition;    //dolab�n standart konumu;
    private Vector3 sabitPozisyon;




    public void SpawnPrefab()
    {

        if (prefabInstance.transform.childCount == 0)
        {
            if (prefab != null)
            {
                spawnedObject = Instantiate(prefab, initialPosition_pf, initialRotation_pf);
                spawnedObject.transform.parent = gPop;
                spawnedObject.transform.localScale = initialScale_pf;
            }
        }
    }

    void DestroyPrefab(GameObject parent)
    {
        // popv2 deki t�m raf� siliyoz child objeleri
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);                                          // Destroy the child object
        }
    }

    void Start()
    {
        sabitPozisyon = gPop.position;      // raf�n yuar�daki pozisyonunu ana pozisyon olarak ald�m
        initialPosition_pf = prefab.transform.position;
        initialRotation_pf = prefab.transform.rotation;
        initialScale_pf = prefab.transform.localScale;
    }

    void OnMouseDown()
    {
        if (gameObject.name == ("glassTrigger"))
        {
            StartCoroutine(openCloset());   // t�kland���nda smooth olarak a�a�� inmesini sa�layan fonksiyonu �a��rd�m
        }
    }

    public void kopruFonksiyonu()
    {
        StartCoroutine(closeCloset());   // t�m raf� tekrar yukar� yollar ama ba�ka script taraf�ndan tetiklenir
    }


    IEnumerator openCloset()
    {
        SpawnPrefab();

        float elapsedTime = 0f;
        float duration = 0.5f; // Geri d�nme s�resif

        Vector3 currentPosition = gPop.position;

        while (elapsedTime < duration)
        {
            gPop.position = Vector3.Lerp(currentPosition, hedef, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Tam olarak ba�lang�� pozisyonuna ayarla (olayl� hatalar� d�zeltmek i�in)
        gPop.position = hedef;
        initialPosition = gPop.position;           // yerle�tirdikten sonraki pozisyonunu ana pozsiyonu olarak kaydettim

    }


    IEnumerator closeCloset()
    {

        float elapsedTime = 0f;
        float duration = 0.5f; // Geri d�nme s�resi

        Vector3 currentPosition = gPop.position;

        while (elapsedTime < duration)
        {
            gPop.position = Vector3.Lerp(currentPosition, sabitPozisyon, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Tam olarak ba�lang�� pozisyonuna ayarla (olayl� hatalar� d�zeltmek i�in)
        gPop.position = sabitPozisyon;
        initialPosition = gPop.position;           // yerle�tirdikten sonraki pozisyonunu ana pozsiyonu olarak kaydettim
                                                   //Debug.Log("close");
        DestroyPrefab(prefabInstance);
    }

}


