using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool gotKey = false;

    private float X;
    private float Y;
    private float Sensitivity = 1500;
    [SerializeField] private GameObject camera;
    private Rigidbody rb;

    //[SerializeField] private float camSpeed = 1000.0f;
    [SerializeField] private float moveSpeed = 0.5f;

    [SerializeField] private float timeOpenDoor = 1f;
    [SerializeField] private GameObject porte;
    [SerializeField] private GameObject serrurePorte;

    

    // Start is called before the first frame update
    void Start()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        X = euler.x;
        //bloque la souris a l'interieur de l'écran de jeu
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       camera.transform.position = gameObject.transform.position;
       MouseMove();
       IsInput();
    }

    private void MouseMove()
    {
        const float xMin = -90f;
        const float xMax = 90f;
        //Gere le deplacement de la caméra avec la souris

        Y += Input.GetAxis("Mouse Y") * (Sensitivity * Time.deltaTime);
        X += Input.GetAxis("Mouse X") * (Sensitivity * Time.deltaTime);

        //camera.transform.Rotate(X,0,0);
        //camera.transform.rotation = Quaternion.Euler((Mathf.Clamp(transform.rotation.x, xMin, xMax)), transform.position.y, transform.position.z);



        camera.transform.rotation = Quaternion.Euler(-Y, X, 0.0f);
        transform.rotation = Quaternion.Euler(0, Y, 0.0f);
    }

    private void IsInput()
    {

        if (Input.GetAxis("Horizontal") < 0)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime) ;

        if (Input.GetAxis("Horizontal") > 0)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (Input.GetAxis("Vertical") < 0)
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if (Input.GetAxis("Vertical") > 0)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("collectible"))
        {
            Destroy(collision.gameObject);
            gotKey = true;
        }

        if(collision.gameObject.CompareTag("porte") && gotKey == true)
        {
            serrurePorte.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            serrurePorte.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

            StartCoroutine(OpenDoor());       
        }

        if(collision.gameObject.CompareTag("flag"))
        {
            Pause();
        }
    }

    IEnumerator OpenDoor()
    {
       yield return new WaitForSeconds(timeOpenDoor);
        Destroy(porte);
    }

    private void Play()
    {
        Time.timeScale = 1;
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }
}
