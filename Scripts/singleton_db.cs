using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleton_db : MonoBehaviour
{
    public static singleton_db instance;

    public bool readyToShake = false;
    public bool readyToMix = false;

    public bool location1 = false;
    public bool location2 = false;
    public bool location3 = false;
    public bool location4 = false;


    public bool isThereAnyObjectStanding = false;    // ortada bir obje var ve o kalkmadan diðer ui leri aktif etmiyorum

    public GameObject AddToThisObject;      // add' butonuna basmadan önce son basýlan objeye ekleme yapýalcak


    public GameObject selectedOne;

    public GameObject canva_other;
    public GameObject canva_bardak;
    public GameObject canva_mix;
    public GameObject canva_shaker;

    public GameObject outline;
    public GameObject o1;
    public GameObject o2;
    public GameObject o3;
    public GameObject o4;

    //public bool shakerAnim = false;

    private void Awake()
    {
        instance = this;
    }

    public void fonk(byte sayac)
    {
        if (!selectedOne.CompareTag("bardak") || !selectedOne.CompareTag("mix") || !selectedOne.CompareTag("shaker"))
            canva_other.SetActive(false);

        switch (sayac)
        {
            case 1:
                o1.SetActive(true);
                o2.SetActive(false);
                o3.SetActive(false);
                o4.SetActive(false);
                break;
            case 2:
                o1.SetActive(false);
                o2.SetActive(true);
                o3.SetActive(false);
                o4.SetActive(false);
                break;
            case 3:
                o1.SetActive(false);
                o2.SetActive(false);
                o3.SetActive(true);
                o4.SetActive(false);
                break;
            case 4:
                o1.SetActive(false);
                o2.SetActive(false);
                o3.SetActive(false);
                o4.SetActive(true);
                break;
        }
        outline.SetActive(true);
    }

    public void delete_outline()
    {
        outline.SetActive(false);
    }

    

}
