using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_deneme : MonoBehaviour
{
  
    //public char key;

   public float transitionDuration = 0.8f;
    public float elapsedTime = 0f;

    public Vector3 cameraLeft_P = new Vector3(1.23000002f, 4.55000019f, 4.94999981f);
    public Vector3 cameraLeft_R = new Vector3(6.2076664f, 106.93116f, 359.821991f);

    public Vector3 cameraRight_P = new Vector3(-1.71000004f, 4.8499999f, 6f);
    public Vector3 cameraRight_R = new Vector3(0.469024599f, 259.654022f, 358.774384f);

    public Vector3 cameraDown_P = new Vector3(-0.219999999f, 2.79999995f, 5.73999977f);
    public Vector3 cameraDown_R = new Vector3(5.28596544f, 180.364243f, 0.0255659856f);

    public Vector3 cameraStock_P = new Vector3(-0.219999999f, 4.78000021f, 6.32999992f);
    public Vector3 cameraStock_R = new Vector3(5.28596544f, 180.364243f, 0.0255659856f);

    public Vector3 cameraZoom_P = new Vector3(-0.230000004f, 5.11999989f, 5.23000002f);
    public Vector3 cameraZoom_R = new Vector3(18.5637074f, 180.370438f, 0.026854502f);

    void Update()
    {




        if (Input.GetKey(KeyCode.A))
            SetPosition(cameraLeft_P, Quaternion.Euler(cameraLeft_R));
        if (Input.GetKey(KeyCode.D))
            SetPosition(cameraRight_P, Quaternion.Euler(cameraRight_R));
        if (Input.GetKey(KeyCode.S))
            SetPosition(cameraDown_P, Quaternion.Euler(cameraDown_R));
        if (Input.GetKey(KeyCode.W))
            SetPosition(cameraStock_P, Quaternion.Euler(cameraStock_R));
        if (Input.GetKey(KeyCode.Z))
            SetPosition(cameraZoom_P, Quaternion.Euler(cameraZoom_R));




    }




    public void SetPosition(Vector3 newPosition, Quaternion newRotation)
    {


         transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime / transitionDuration);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime / transitionDuration);

    }



}




