using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixOrShakeDone : MonoBehaviour
{
    public GameObject filtre;
    public ParticleSystem PS;
    public Behaviour pouringScript;
    public Animator anim;
    public Behaviour shakerFill_execute;

    void Start()
    {
        PS.Stop();
        pouringScript.enabled = false;
    }

    public void Done()
    {
        pouringScript.enabled = true;
        filtre.SetActive(true);
        anim.SetBool("filter", true);
        shakerFill_execute.enabled = false;
    }
    public void exitMosd()
    {
        pouringScript.enabled = false;
        filtre.SetActive(false);
        anim.SetBool("filter", false);
        PS.Stop();
        shakerFill_execute.enabled = true;

    }
}
