using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehaviour : MonoBehaviour
{
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float deceleration = 0.75f;
    [SerializeField] private Rigidbody rigidBody;
    private Vector3 desiredDir;
    private float currentSpeed;
    private bool shouldBrake;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        if (direction.magnitude < 1f)
        {
            shouldBrake = true;
        }
        Debug.Log($"{name}: Dir magnitude {direction.magnitude}");
        desiredDir = direction;

    }

    private void FixedUpdate()
    {
        var currentHorizontalVelocity = rigidBody.velocity;
        currentHorizontalVelocity.y = 0;
        var currentSpeed = currentHorizontalVelocity.magnitude;


        if (currentSpeed < maxSpeed && desiredDir.magnitude > 0)
            rigidBody.AddForce(desiredDir.normalized * acceleration, ForceMode.Force);
 
        if (shouldBrake)
        {
            rigidBody.AddForce(-rigidBody.velocity * deceleration, ForceMode.Acceleration);
            shouldBrake = false;
            Debug.Log($"{name}: Character hit brake!\tCurrent Speed is {currentSpeed}");
        }
    }
}