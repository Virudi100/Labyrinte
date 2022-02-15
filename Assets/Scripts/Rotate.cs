using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float speed = 35f;

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        gameObject.transform.Rotate(0, 1 * speed * Time.deltaTime, 0);
    }
}
