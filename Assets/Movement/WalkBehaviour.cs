using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehaviour : MonoBehaviour
{
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private Rigidbody rigidBody;
    private Vector3 desiredDir;

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
        if (desiredDir.magnitude > 0)       
            rigidBody.AddForce(desiredDir.normalized * acceleration, ForceMode.Force);       
        else
            rigidBody.velocity -= rigidBody.velocity * deceleration * Time.fixedDeltaTime;


        if (rigidBody.velocity.magnitude > moveSpeed)
            rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
    }
}