using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheContent : MonoBehaviour
{
    // ONLY MIX SHAKER AND BARDAK TAGGED OBJECTS CAN HAVE THIS SCRIPT
    
    
    public string[] bilesenler_content = new string[11];       // kullanicinin kullanabileceði max sývý sayýsý (eðer en az 1cl koymazsa sayýlmýyor)

    public int[] miktarlar_content = new int[11];               //sývýnýn miktarýný kaydeden array.

    public ParticleSystem PS;

    public void test()
    {
        for (int a = 0; a < 11; a++)
        {
            Debug.Log("LINE: " + a + " ALCOHOL: " + bilesenler_content[a] + " AMOUNT: " + miktarlar_content[a]);
        }
    }





}
