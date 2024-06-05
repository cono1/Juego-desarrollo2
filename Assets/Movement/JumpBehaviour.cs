using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float jumpAcceleration = 30f;
    [SerializeField] private Rigidbody rigidBody;
    //private float verticalSpeed = 0f;
    private bool wantToJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        if(rigidBody == null)
        {
            Debug.LogError($"{name}: The {nameof(rigidBody)} is missing for the {nameof(JumpBehaviour)} script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (wantToJump)
        {
            rigidBody.AddForce(Vector3.up * jumpAcceleration, ForceMode.Impulse);
            wantToJump = false;
        }
    }

    public void Jump()
    {
        Debug.Log($"{name}: Jumped!");
        wantToJump = true;
    }

    private void ApplyGravity()
    { 

    }
}
