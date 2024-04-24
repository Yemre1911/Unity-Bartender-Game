using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderSpace4 : MonoBehaviour
{
    public GameObject emptyTheDrink2;

    private  tezgah tzgh; // Global variable to hold the Tezgah script reference


    void OnCollisionEnter(Collision collision)                                          // Çarpýþma
   {
       drag dragScriptNesne = collision.gameObject.GetComponent<drag>();

       if(dragScriptNesne!=null && !dragScriptNesne.enabled)
       {
            tzgh = collision.gameObject.GetComponent<tezgah>();  // objenin tezgah scriptine eriþtim


            if (collision.gameObject.CompareTag("shaker") || collision.gameObject.CompareTag("bardak") || collision.gameObject.CompareTag("mix"))
            {
                if (collision.gameObject.CompareTag("shaker") && singleton_db.instance.readyToShake)
                {
                    showTheMessage();
                }
               /* else
                    tzgh.emptyTheDrink_yes();*/



                if (collision.gameObject.CompareTag("mix") && singleton_db.instance.readyToMix)
                {
                    showTheMessage();
                }
                

                if (collision.gameObject.CompareTag("bardak"))
                {
                    Transform liquidTransform = collision.gameObject.transform.Find("liquid");

                    if (liquidTransform != null && liquidTransform.gameObject.activeSelf)      // bardaðýn içinde liquid var mý  bakýyorum
                    {
                        showTheMessage();
                    }

                }
            }
       }
   }

    public void showTheMessage()
    {
        emptyTheDrink2.SetActive(true);
        Debug.Log("show The Message");
    }

    public void Empty_Yes()
    {
        //tezgah tzgh = collision.gameObject.GetComponent<tezgah>();
        if (tzgh != null)
        {
            tzgh.emptyTheDrink_yes();
        }
    }

    public void Empty_No()
    {
       // tezgah tzgh = collision.gameObject.GetComponent<tezgah>();
        if (tzgh != null)
        {
            tzgh.emptyTheDrink_no();
        }
    }
}
