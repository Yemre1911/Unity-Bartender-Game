using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class glassTrigger : MonoBehaviour
{

    // bardakSpawnOrDelete script2Instance;    // bardakSpawnOrDelete scriptine eriþmek için gerekli kod
   // shaker_animation shakerScript;
    // ----------------------------------------------------------------------------------------------------------

    public GameObject prefabInstance; // içi yok edilecek parentin adresi

    public GameObject prefab; // tüm rafý spawnlayýp yok etmek için
    private GameObject spawnedObject; // spawn için                                         // Spawn bilgileri
    private Vector3 initialPosition_pf;        // ilk pozisyon
    private Quaternion initialRotation_pf;     // ilk rotasyon
    private Vector3 initialScale_pf;           // ilk boyut

    //---------------------------------------------------------------------------------------------------------

    public Transform gPop;  // raf objesinin transform bilgilerini buraya atadým
    public Vector3 hedef = new Vector3(2.68449998f, 4.4000001f, 1.98819995f);
    private Vector3 initialPosition;    //dolabýn standart konumu;
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
        // popv2 deki tüm rafý siliyoz child objeleri
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);                                          // Destroy the child object
        }
    }

    void Start()
    {
        sabitPozisyon = gPop.position;      // rafýn yuarýdaki pozisyonunu ana pozisyon olarak aldým
        initialPosition_pf = prefab.transform.position;
        initialRotation_pf = prefab.transform.rotation;
        initialScale_pf = prefab.transform.localScale;
    }

    void OnMouseDown()
    {
        if (gameObject.name == ("glassTrigger"))
        {
            StartCoroutine(openCloset());   // týklandýðýnda smooth olarak aþaðý inmesini saðlayan fonksiyonu çaðýrdým
        }
    }

    public void kopruFonksiyonu()
    {
        StartCoroutine(closeCloset());   // tüm rafý tekrar yukarý yollar ama baþka script tarafýndan tetiklenir
    }


    IEnumerator openCloset()
    {
        SpawnPrefab();

        float elapsedTime = 0f;
        float duration = 0.5f; // Geri dönme süresif

        Vector3 currentPosition = gPop.position;

        while (elapsedTime < duration)
        {
            gPop.position = Vector3.Lerp(currentPosition, hedef, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Tam olarak baþlangýç pozisyonuna ayarla (olaylý hatalarý düzeltmek için)
        gPop.position = hedef;
        initialPosition = gPop.position;           // yerleþtirdikten sonraki pozisyonunu ana pozsiyonu olarak kaydettim

    }


    IEnumerator closeCloset()
    {

        float elapsedTime = 0f;
        float duration = 0.5f; // Geri dönme süresi

        Vector3 currentPosition = gPop.position;

        while (elapsedTime < duration)
        {
            gPop.position = Vector3.Lerp(currentPosition, sabitPozisyon, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Tam olarak baþlangýç pozisyonuna ayarla (olaylý hatalarý düzeltmek için)
        gPop.position = sabitPozisyon;
        initialPosition = gPop.position;           // yerleþtirdikten sonraki pozisyonunu ana pozsiyonu olarak kaydettim
                                                   //Debug.Log("close");
        DestroyPrefab(prefabInstance);
    }

}


