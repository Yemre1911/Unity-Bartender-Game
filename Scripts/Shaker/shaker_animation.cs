using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaker_animation : MonoBehaviour
{
    public Animator shaker_animator;
    public MeshCollider meshCollider;




    public void openShaker()
    {
        meshCollider.enabled = false;

        shaker_animator.SetBool("open", true);
        shaker_animator.SetBool("close", false);
        StartCoroutine(beklet());
        
    }

    public void closeShaker()
    {
        meshCollider.enabled = false;

        shaker_animator.SetBool("close", true);
        shaker_animator.SetBool("open", false);
        StartCoroutine(beklet());
    }

    IEnumerator beklet()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("bekleme tamamlandi");
        meshCollider.enabled = true;


    }

}
