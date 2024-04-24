using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mix_anim : MonoBehaviour
{
    public Animator anim;
    public GameObject spoonTrail;
    public GameObject mix_button;

    public void mixButton()
    {
        if (singleton_db.instance.readyToMix)
        {
            mix_button.SetActive(false);
            spoonTrail.SetActive(true);
            anim.SetBool("mix", true);
            StartCoroutine(beklet());
        }
        else
            Debug.Log("MIXERI DOLDURMADAN MIXLEYEMEZSIN");

    }

    IEnumerator beklet()
    {
        yield return new WaitForSeconds(3);
        mixOrShakeDone mosd = GameObject.FindWithTag("mix").GetComponent<mixOrShakeDone>();
        spoonTrail.SetActive(false);

        mosd.Done();
    }
}
