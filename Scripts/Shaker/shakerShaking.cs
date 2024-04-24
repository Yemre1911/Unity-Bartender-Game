using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shakerShaking : MonoBehaviour
{

    public GameObject Shake_button;

    public Animator shaker_anim;
    public GameObject realShaker;
    public GameObject fakeShaker;

    public Image iceUI;
    public GameObject iceEffect;
    public GameObject canvaOther;

    private float fadeDuration = 25f; // Duration in seconds for the fade

    public void shake()
    {
        if (singleton_db.instance.readyToShake)
        {
            Shake_button.SetActive(false);
            canvaOther.SetActive(false);
            iceEffect.SetActive(true);
            shaker_animation nesne = GetComponent<shaker_animation>();
            nesne.closeShaker();
            StartCoroutine(FadeOutRoutine(true));
            StartCoroutine(beklet(1));
        }
        else
            Debug.Log("SHAKERI DOLDURMADAN SAHKELEYEMEZSIN");
    }

    IEnumerator beklet(float wait)
    {
        yield return new WaitForSeconds(wait);

        if (wait > 2)
        {
            realShaker.SetActive(true);
            fakeShaker.SetActive(false);

            shaker_animation nesne = GetComponent<shaker_animation>();
            nesne.openShaker();
            canvaOther.SetActive(true);
            StartCoroutine(FadeOutRoutine(false));
            mixOrShakeDone mosd = GameObject.FindWithTag("shaker").GetComponent<mixOrShakeDone>();
            mosd.Done();
        }
        else
        {
            realShaker.SetActive(false);
            fakeShaker.SetActive(true);

            shaker_anim.SetBool("shake", true);
            StartCoroutine(beklet(13));
        }
    }
    IEnumerator FadeOutRoutine(bool swc)
    {
        float elapsed = 0.0f;

        // Get the current color of the image
        Color originalColor = iceUI.color;
        float startAlpha = originalColor.a;

        if (swc == true)
        {
            fadeDuration = 25f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;

                // Calculate the new alpha value
                float newAlpha = Mathf.Clamp01(elapsed / fadeDuration);
                iceUI.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

                yield return null; // Wait for the next frame
            }
        }
        else
        {
            fadeDuration = 4f;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;

                // Calculate the new alpha value
                float newAlpha = Mathf.Clamp01(startAlpha - (elapsed / fadeDuration));
                iceUI.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

                yield return null; // Wait for the next frame
            }
            iceEffect.SetActive(false);

        }

    }

    

 

}
