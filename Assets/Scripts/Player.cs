using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool gotKey = false;

    private float X;
    private float Y;
    private float Sensitivity = 100;
    [SerializeField] private GameObject camera;

    //[SerializeField] private float camSpeed = 1000.0f;
    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private float timeOpenDoor = 1f;
    [SerializeField] private GameObject porte;
    [SerializeField] private GameObject serrurePorte;
    [SerializeField] private GameObject gameoverCanvas;

    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource winSound;


    // Start is called before the first frame update
    void Start()
    {
        //bloque la souris a l'interieur de l'écran de jeu
        Cursor.lockState = CursorLockMode.Locked;

        gameoverCanvas.SetActive(false);

    }
    

    // Update is called once per frame
    void Update()
    {
       
       MouseMove();
       IsInput();
    }

    private void MouseMove()
    {
        //Gere le deplacement de la caméra avec la souris

        Y += Input.GetAxis("Mouse Y") * (Sensitivity * Time.deltaTime);
        X += Input.GetAxis("Mouse X") * (Sensitivity * Time.deltaTime);

        camera.transform.rotation = Quaternion.Euler(Mathf.Clamp(-Y,-90f,90f), X, 0.0f);
        transform.rotation = Quaternion.Euler(0, X, 0.0f);
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
            GameOverWin();
        }

        if(collision.gameObject.CompareTag("ennemi"))
        {
            GameOverDeath();
            collision.gameObject.GetComponent<AudioSource>().Pause();
        }
    }

    IEnumerator OpenDoor()
    {
       yield return new WaitForSeconds(timeOpenDoor);
       porte.GetComponent<Animator>().SetTrigger("opendoor");
    }

    private void GameOverDeath()
    {
        Pause();
        gameoverCanvas.SetActive(true);
        camera.GetComponent<AudioSource>().Pause();
        deathSound.Play();
    }

    private void GameOverWin()
    {
        Pause();
        gameoverCanvas.SetActive(true);
        camera.GetComponent<AudioSource>().Pause();
        winSound.Play();
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
