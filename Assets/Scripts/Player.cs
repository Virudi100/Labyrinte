using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float X;
    private float Y;
    private float Sensitivity = 1500;
    [SerializeField] private GameObject camera;
    private Rigidbody rb;

    //[SerializeField] private float camSpeed = 1000.0f;
    [SerializeField] private float moveSpeed = 0.5f;

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
        //Gere le deplacement de la caméra avec la souris
        const float MIN_X = 0.0f;
        const float MAX_X = 360.0f;
        const float MIN_Y = 0.0f;
        const float MAX_Y = 360.0f;

        X += Input.GetAxis("Mouse X") * (Sensitivity * Time.deltaTime);
        if (X < MIN_X) X += MAX_X;
        else if (X > MAX_X) X -= MAX_X;

        Y += Input.GetAxis("Mouse Y") * (Sensitivity * Time.deltaTime);
        if (Y < MIN_Y) Y += MAX_Y;
        else if (Y > MAX_Y) Y -= MAX_Y;

        camera.transform.rotation = Quaternion.Euler(-Y, X, 0.0f);
        transform.rotation = Quaternion.Euler(0, X, 0.0f);
    }

    private void IsInput()
    {
        //rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") < 0)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime) ;

        if (Input.GetAxis("Horizontal") > 0)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (Input.GetAxis("Vertical") < 0)
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if (Input.GetAxis("Vertical") > 0)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
