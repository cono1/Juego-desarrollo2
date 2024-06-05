using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehaviour : MonoBehaviour
{
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float deceleration = 0.75f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Rigidbody rigidBody;
    private Vector3 desiredDir;
    private float currentSpeed;
    private bool shouldBrake;
    [SerializeField] private float maxAngleRotation = 140;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        var angle = Vector3.Angle(rigidBody.velocity, direction);

        shouldBrake = direction.magnitude < 0.01f;
        
        Debug.Log($"{name}: Dir magnitude {direction.magnitude}");

        desiredDir = transform.TransformDirection(direction);

        if (angle > maxAngleRotation)
        {
            desiredDir = Vector3.zero;
        }
    }


    private void FixedUpdate()
    {
        var currentHorizontalVelocity = rigidBody.velocity;
        currentHorizontalVelocity.y = 0;
        var currentSpeed = currentHorizontalVelocity.magnitude;
        desiredDir.y = 0;

        float angle = Vector3.SignedAngle(transform.forward, desiredDir, transform.up);

        if (currentSpeed < maxSpeed && desiredDir.magnitude > 0 && !shouldBrake)
        {
            rigidBody.AddForce(desiredDir * acceleration, ForceMode.Force);

            //Quaternion toRotation = Quaternion.LookRotation(desiredDir, Vector3.up); //https://www.youtube.com/watch?v=BJzYGsMcy8Q&list=PLx7AKmQhxJFaj0IcdjGJzIq5KwrIfB1m9&index=2
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            transform.Rotate(transform.up, angle * rotationSpeed * Time.deltaTime);

            //transform.Rotate(transform.up * desiredDir.x * (rotationSpeed * Time.deltaTime));
        }


        if (shouldBrake)
        {
            rigidBody.AddForce(-rigidBody.velocity * deceleration, ForceMode.Acceleration);
            shouldBrake = false;
            Debug.Log($"{name}: Character hit brake!\tCurrent Speed is {currentSpeed}");
        }
    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD

    private void OnGUI()
    {
        GUI.Label(new Rect(20, Screen.height / 2, 200, 20), "Velocity Magnitude: " + rigidBody.velocity.magnitude.ToString("F2"));
    }
#endif
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, rigidBody.velocity);
    }
}