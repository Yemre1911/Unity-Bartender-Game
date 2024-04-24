using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using UnityEngine.UI;

public class shakerFill_execute : MonoBehaviour
{
    public GameObject UIsayac;
    public TMP_Text mililitre1;
    public TMP_Text mililitre2;
    public Image filledImage;       // UI codes
    private float fillSpeed = 0.2f; // Speed of filling  
    public GameObject JiggerImage;


    public Collider myCollider; // is triggeri kontrol etmek i�in

    private int tempSiralama = -1;

    public GameObject liquid; // y�kselecek s�v�
    private float scaleFactor = 1f; // You can adjust the scale factor as needed
    private float scaleSpeed = 0.05f; // You can adjust the scale speed as needed
    private float maxlimit = 0.90f;

    private bool isColliding =false;
    private bool click = false;
    private int bilesenSiralamasi;
    private float temasSuresi = 0f;
    private float temasSuresi_reset = 0f;

    private float gecmisTemasSuresi = 0f;
    private string oncekiSivi="x";  // d�k�len s�v�dan �nceki s�v�n�n de�erini al�r
    private float sonuc;
    private string dokulenSiviAdi;


    //  COLOR CHANGING VAR�ABLES
    
    private Renderer liquidRenderer; // Renderer component of the liquid GameObject
    private Color initialLiquidColor; // Initial color of the liquid
    public float blendingSpeed = 0.5f; // Speed of the color blending
    private Color targetColor; // The color to blend towards
    private bool isFirstCollision = true; // Flag to indicate the first collision


    //==============================================================

    private string[] bilesenler = new string[11];       // kullanicinin kullanabilece�i max s�v� say�s� (e�er en az 1cl koymazsa say�lm�yor)

    private int[] miktarlar = new int[11];               //s�v�n�n miktar�n� kaydeden array.

    //==============================================================
    void OnMouseDown()
    {
        TheContent content = GetComponent<TheContent>();
        content.test();
    }


    void OnMouseDrag()
    {
        myCollider.isTrigger = false;
    }
    void OnMouseUp()
    {
        myCollider.isTrigger = true;
        JiggerFill(false);
    }


    // =============== ALKOL DI�I ��ER�KOLER ==================== //


    void Start()
    {
        if (!gameObject.CompareTag("shaker"))
        {
            // Get the Renderer component of the liquid GameObject
            liquidRenderer = liquid.GetComponent<Renderer>();
            // Get the initial color of the liquid
            initialLiquidColor = liquidRenderer.material.color;
        }

        myCollider.isTrigger = true;

        for (int a = 0; a < 11; a++)
        {
            bilesenler[a] = "x";
            miktarlar[a]=0;
        }
    }


    void OnTriggerStay(Collider other)          //d�k�lmeye ba�lad�
    {
        if (enabled)
        {
            if (other.CompareTag("PS"))
            {
                isColliding = true;
                //=============================== COLOR CHANGING CODES ==============================================================

                if (!gameObject.CompareTag("shaker"))
                {
                    if (isFirstCollision)
                    {
                        // Check if the colliding object has a Renderer component
                        Renderer otherRenderer = other.GetComponent<Renderer>();
                        if (otherRenderer != null)
                        {
                            // Get the color of the colliding object
                            Color otherColor = otherRenderer.material.color;

                            // Set the liquid's color to the colliding object's color immediately
                            liquidRenderer.material.color = otherColor;

                            // Set isFirstCollision flag to false to prevent further immediate color changes
                            isFirstCollision = false;
                        }
                    }
                    else
                    {
                        Renderer otherRenderer = other.GetComponent<Renderer>();
                        if (otherRenderer != null)
                        {
                            // Get the color of the colliding object
                            Color otherColor = otherRenderer.material.color;


                            // Mix the colors
                            targetColor = MixColors(initialLiquidColor, otherColor);

                            // Interpolate between current color and target color
                            Color blendedColor = Color.Lerp(liquidRenderer.material.color, targetColor, blendingSpeed * Time.deltaTime);

                            // Apply the blended color to the liquid
                            liquidRenderer.material.color = blendedColor;

                        }
                    }
                }
                // ====================================================================================

                UIsayac.SetActive(true);

                dokulenSiviAdi = other.gameObject.name;    // d�k�len s�v�n�n ad�n� ald�k
                bilesenSiralamasi = kontrol(dokulenSiviAdi);    //ad� kontrol edelim, ilk defa m� d�k�l�yor
                if (tempSiralama != bilesenSiralamasi)
                {
                    temasSuresi = miktarlar[bilesenSiralamasi];
                    tempSiralama = bilesenSiralamasi;
                }
                if (gameObject.CompareTag("mix") || gameObject.CompareTag("bardak"))
                {
                    liquid.SetActive(true);

                    if (gameObject.CompareTag("mix"))
                    {
                        miktarAyarla(bilesenSiralamasi, temasSuresi / 2);
                        ScaleAroundPivot(liquid.transform, liquid.transform.position, Vector3.up, scaleFactor * Time.deltaTime * scaleSpeed);
                    }
                    if (gameObject.CompareTag("bardak"))
                    {
                        miktarAyarla(bilesenSiralamasi, temasSuresi);
                        scaleSpeed = 0.4f;
                        if (gameObject.name == "cocktailGlass")
                            ScaleLiquidAroundPivot(liquid.transform, liquid.transform.position, scaleFactor, scaleSpeed);

                        else
                            ScaleAroundPivot(liquid.transform, liquid.transform.position, Vector3.up, scaleFactor * Time.deltaTime * scaleSpeed);
                    }


                }
                else
                {
                    miktarAyarla(bilesenSiralamasi, temasSuresi);
                }
                temasSuresi += Time.deltaTime;
                string timeText;
                if (gameObject.CompareTag("mix"))
                {
                    float a = temasSuresi / 2;
                    timeText = a.ToString("F1");
                }
                else
                    timeText = temasSuresi.ToString("F1");

                mililitre1.text = timeText + " cl";
                mililitre2.text = timeText + " cl";
                JiggerFill(true);
            }
        }
    }


    private Color MixColors(Color color1, Color color2)
    {
        // You can implement your own mixing algorithm here
        // For simplicity, let's just take the average of the RGB values
        Color mixedColor = new Color(
            (color1.r + color2.r) / 2f,
            (color1.g + color2.g) / 2f,
            (color1.b + color2.b) / 2f,
            245f / 255f // Set alpha to 206 (assuming you want alpha in the range 0-1)
        );
        return mixedColor;
    }

    void ScaleAroundPivot(Transform target, Vector3 pivot, Vector3 axis, float scaleFactor)       // s�v� y�kseltici
    {


        Vector3 newScale = target.localScale;
        if (gameObject.CompareTag("mix"))
            maxlimit = 0.90f;

        if (gameObject.CompareTag("bardak"))
        {
            if (gameObject.name == "beerGlass")
                maxlimit = 1.25f;
            else if (gameObject.name == "longOndrck")
                maxlimit = 2.48f;
            else if (gameObject.name == "shotGlass")
                maxlimit = 1.24f;
            else if (gameObject.name == "cocktailGlass")
                maxlimit = 0.66f;
            else
                maxlimit = 2f;
        }

        if (newScale.z <= maxlimit)
        {
            newScale.z += scaleFactor;
            target.localScale = newScale;   // apply the new scale
        }
    }

    void ScaleLiquidAroundPivot(Transform liquidTransform, Vector3 pivotPoint, float scaleFactor, float scaleSpeed) // cocktailgass code
    {
        // Calculate the new scale based on time and scale speed
        float newScale = liquidTransform.localScale.y + scaleFactor * Time.deltaTime * scaleSpeed;

        // Check if the new scale exceeds the limit
        if (newScale > 1.2f)
        {
            newScale = 1.2f; // Limit the scale to 0.66
            scaleSpeed = 0f; // Stop scaling
        }

        // Calculate the difference in scale from the pivot point
        float scaleDifference = newScale / liquidTransform.localScale.y;

        // Calculate the position difference from the pivot point
        Vector3 positionDifference = (pivotPoint - liquidTransform.position) * (1 - scaleDifference);

        // Apply the new scale and position
        liquidTransform.localScale *= scaleDifference;
        liquidTransform.position += positionDifference;
    }

    int kontrol(string siviAdiDokulen)
    {
        int x; // for d�ng�s� i�in
        bool swc = false;
        int siraNumarasi=0;
        int sayac = 0; // e�er saayc 10 olursa bo� yer yok demektir ve shaker dolmu� anlam�na gelir, yeni s�v� atanamaz

        for (x = 0; x <= 10; x++)                  // d�k�len s�v� daha �nce d�k�lm�� mi diye kontrol
        {
            if (bilesenler[x] == siviAdiDokulen)
            {
                swc = true;
                siraNumarasi = x;   // sivi daha �nce d�k�lm�� ve hangi s�rada oldu�unu bulduk
                break;
            }
        }
        
        x = 0;
        if (swc == false)            // d�k�len s�v� daha �nce d�k�lmemi�se ya shaker bo�tur, ya da ilk defa bu sivi d�k�l�yordur 
        {
            for (x = 0; x <= 10; x++)
            {
                if (bilesenler[x] == "x")
                {
                    siraNumarasi = x;
                    break;
                }
                else
                {
                    sayac++;
                }
            }
        }

        if (sayac == 11)
            Debug.Log("shaker 11 adetten fazla bile�en alamaz!!!");
        else
            bilesenler[siraNumarasi] = siviAdiDokulen;     // gerekli atama yap�ld�




        TheContent content = GetComponent<TheContent>();        // i�erik scriptine yollad�m
        content.bilesenler_content = bilesenler;

        return siraNumarasi;

    }


    void miktarAyarla(int bilesenSiralamasi2, float temasSuresi2)
    {
        int floatDonusturucu = (int)temasSuresi2;
        miktarlar[bilesenSiralamasi2] = floatDonusturucu;


        TheContent content = GetComponent<TheContent>();    // i�erik scriptine yollad�m
        content.miktarlar_content = miktarlar;

        if (floatDonusturucu>0)                // mix veya shakerin i�inde s�v� varsa animasyon ba�lat�lacak
        {
            if (gameObject.CompareTag("shaker"))
                singleton_db.instance.readyToShake = true;
            if (gameObject.CompareTag("mix"))
                singleton_db.instance.readyToMix = true;
        }
    }

    void JiggerFill(bool swc)
    {
       
        if (swc)
        {
            JiggerImage.SetActive(true);
            // Example: Incremental fill over time
            if(!gameObject.CompareTag("mix"))
                filledImage.fillAmount += fillSpeed * Time.deltaTime;
            else
                filledImage.fillAmount += fillSpeed * (Time.deltaTime / 2);


            // Clamp the fill amount between 0 and 1
            filledImage.fillAmount = Mathf.Clamp(filledImage.fillAmount, 0, 1);
        }
         else
        {
            filledImage.fillAmount = 0;
            JiggerImage.SetActive(false);
            UIsayac.SetActive(false);

        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("PS"))
        {
            Debug.Log("COLLISION EXIT");
            JiggerFill(false);
            isColliding = false;
        }
    }

}







