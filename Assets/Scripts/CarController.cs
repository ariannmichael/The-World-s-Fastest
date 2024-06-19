using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody rb;

    // Movement variables
    public float maxSpeed;
    public float forwardAccel = 8f;
    public float reverseAccel = 4f;
    private float speedInput;
    public float turnStrength = 150f;
    private float turnInput;

    private float dragOnGround;
    
    // Start is called before the first frame update
    void Start()
    {
        this.rb.transform.parent = null;
        this.dragOnGround = this.rb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;

        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel;
        }

        turnInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        this.rb.drag = this.dragOnGround;
        rb.AddForce(transform.forward * (speedInput * 1000f));

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        transform.position = this.rb.position;

        if (speedInput != 0)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles +
                new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Math.Sign(speedInput) * (rb.velocity.magnitude / maxSpeed), 0f)
            );
        }
    }
}
