using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isFilling_PS_collider : MonoBehaviour          // bu scripts shaker/mixBarda��/bardaklara eklenir ve i�ine dolan alkolu hesaplar ve bir output ��kar�r
{
    private string output;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("G�R�S");

        string collidedObjectTag = other.gameObject.name; // Retrieving the tag of the collided object

       // Debug.Log("Alkol: " + collidedObjectTag);

    }

    
}
