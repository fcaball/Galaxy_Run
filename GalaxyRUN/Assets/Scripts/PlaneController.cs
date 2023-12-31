using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float throttleIncrement = 0.01f;
    public float maxThrottle = 200f;
    public float responsiveness = 10f;
    private float speed = 5f;
    private float throttle, pitch, yaw, roll;

    Rigidbody rb;

    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space))
            throttle += throttleIncrement;
        else if (Input.GetKey(KeyCode.LeftControl))
            throttle -= throttleIncrement;

        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }

    void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrottle * throttle * speed);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(transform.forward * roll * responseModifier);
    }
}
