using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateSqure : Platform
{
    public float angle;
    public float speed = 90;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = 90;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Rotate(Vector3.up, 90 * Time.deltaTime, Space.Self);
    }

    private void FixedUpdate()
    {
        angle = Time.fixedDeltaTime * speed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * angle));
    }

}
