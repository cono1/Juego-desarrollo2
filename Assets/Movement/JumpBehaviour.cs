using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float jumpAcceleration = 30f;
    [SerializeField] private float verticalSpeed = 0.1f;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Transform feetPivot;
    [SerializeField] private float groundedDistance = 0.2f;
    [SerializeField] private float gravity = 5f;
    [SerializeField] private LayerMask floor;
    private float currentVerticalSpeed = 0f;
    private bool wantToJump = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        if(rigidBody == null)
        {
            Debug.LogError($"{name}: The {nameof(rigidBody)} is missing for the {nameof(JumpBehaviour)} script.");
        }
    }

    private void FixedUpdate()
    {
        if (wantToJump && CanJump())
        {
            JumpAction();
        }

        ApplyGravity();
    }

    public void Jump()
    {
        Debug.Log($"{name}: Jump requested");

        if(CanJump())
        wantToJump = true;
    }

    private void JumpAction()
    {
        Debug.Log($"{name}: Jumped");
        currentVerticalSpeed = verticalSpeed;
        rigidBody.AddForce(Vector3.up * currentVerticalSpeed * jumpAcceleration, ForceMode.Impulse);
        wantToJump = false;
    }
    private bool CanJump()
    {
        if (!feetPivot)
        {
            Debug.LogWarning($"{name}: {nameof(feetPivot)} is null!");
            return false;
        }

        return (Physics.Raycast(feetPivot.position, Vector3.down, out RaycastHit hit, groundedDistance, floor));
    }

    private void ApplyGravity()
    {
        if (!CanJump())
        {
            currentVerticalSpeed -= gravity * Time.fixedDeltaTime;
            rigidBody.AddForce(Vector3.up * currentVerticalSpeed, ForceMode.Force);
        }
    }
}