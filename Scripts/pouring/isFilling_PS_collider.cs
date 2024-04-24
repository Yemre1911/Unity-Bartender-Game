using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isFilling_PS_collider : MonoBehaviour          // bu scripts shaker/mixBardaðý/bardaklara eklenir ve içine dolan alkolu hesaplar ve bir output çýkarýr
{
    private string output;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("GÝRÝS");

        string collidedObjectTag = other.gameObject.name; // Retrieving the tag of the collided object

       // Debug.Log("Alkol: " + collidedObjectTag);

    }

    
}
