using UnityEngine;
using System.Collections;

public class Add_Button : MonoBehaviour
{
    public GameObject bottlePH;

    public Collider shakerCollider; // shakerin boyu fazla oluðu için add seçerken engelliyor

    public GameObject buttonClose;
    public GameObject buttonAdd1;
    public GameObject buttonAdd2;
    public GameObject buttonAdd3;





    public GameObject targetObject; // The GameObject to move
    public float speed = 5.0f; // Speed of the movement

    private Vector3 targetPosition = new Vector3(0.239999995f, 3.81999993f, -1.86000001f);
    private Quaternion targetRotation = new Quaternion(0.554996789f, 0f, 0f, 0.831852496f);

    private Vector3 tp;
    private Quaternion tr;


    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private bool swc = false;

    void Start()
    {
        initialPosition = targetObject.transform.position;
        initialRotation = targetObject.transform.rotation;
    }
    public void OnButtonPress()
    {
        if (targetObject != null)
        {
            bottlePH.SetActive(false);
            shakerCollider.enabled = false;
            swc = false;
            singleton_db.instance.isThereAnyObjectStanding = true;  // ortada bir obje var ve o kalkmadan diðer ui leri aktif etmiyorum
            singleton_db.instance.AddToThisObject = singleton_db.instance.selectedOne;      // add' butonuna basmadan önce son basýlan objeye ekleme yapýalcak




            buttonAdd1.SetActive(false);
            buttonAdd2.SetActive(false);
            buttonAdd3.SetActive(false);

            buttonClose.SetActive(true);
            StartCoroutine(MoveToPosition());
        }
    }

    public void OnClosePress()
    {
        swc = true;
        bottlePH.SetActive(true);
        shakerCollider.enabled = true;
        singleton_db.instance.isThereAnyObjectStanding = false;

        buttonAdd1.SetActive(true);
        buttonAdd2.SetActive(true);
        buttonAdd3.SetActive(true);

        buttonClose.SetActive(false);
        StartCoroutine(MoveToPosition());



    }

    IEnumerator MoveToPosition()
    {
        if(swc==true)
        {
            tp = initialPosition;
            tr = initialRotation; 
        }
        else
        {
            tp = targetPosition;
            tr = targetRotation;
        }
        while (Vector3.Distance(targetObject.transform.position, tp) > 0.01f)
        {
            // Move towards the target position
            targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, tp, speed * Time.deltaTime);

            // Rotate towards the target rotation
            targetObject.transform.rotation = Quaternion.Lerp(targetObject.transform.rotation, tr, speed * Time.deltaTime);

            yield return null;
        }

        // Ensure the final position and rotation are set accurately
        targetObject.transform.position = tp;
        targetObject.transform.rotation = tr;
    }


}
