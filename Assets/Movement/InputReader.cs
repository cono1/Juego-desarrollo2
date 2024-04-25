using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public WalkBehaviour walkBehaviour;

    PlayerInput playerInput;
    InputAction moveAction;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        moveAction.performed += ctx => HandleMoveInput(ctx); // Subscribe to the performed event

    }
    private void Awake()
    {
        if (walkBehaviour == null)
        {
            Debug.LogError($"{name}: {nameof(walkBehaviour)} is null!" +
                           $"\nThis class is dependant on a {nameof(walkBehaviour)} component!");
        }
    }

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (walkBehaviour != null)
            walkBehaviour.Move(moveDirection);
    }
}