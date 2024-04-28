using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehaviour : MonoBehaviour
{
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private Rigidbody rigidBody;
    private Vector3 desiredDir;
    private float currentSpeed;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        desiredDir = direction;
    }

    private void FixedUpdate()
    {
        currentSpeed = Vector3.Magnitude(rigidBody.velocity);

        if (currentSpeed < maxSpeed)
        {
            rigidBody.AddForce(desiredDir.normalized * acceleration, ForceMode.Force);
        }
        else
        {
            rigidBody.velocity -= rigidBody.velocity * deceleration * Time.fixedDeltaTime;
        }
    }
}