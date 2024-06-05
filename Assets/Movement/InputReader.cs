using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public WalkBehaviour walkBehaviour;
    public JumpBehaviour jumpBehaviour;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    private void OnEnable()
    {     
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions.FindAction("Move");
        moveAction.performed += HandleMoveInput; // Subscribe to the performed event
        moveAction.canceled += HandleMoveInputCanceled;

        jumpAction = playerInput.actions.FindAction("Jump");
        jumpAction.performed += HandleJumpInput;
    }

    private void Start()
    {
    }
    private void Awake()
    {
        if (walkBehaviour == null)
        {
            Debug.LogError($"{name}: {nameof(walkBehaviour)} is null!" +
                           $"\nThis class is dependant on a {nameof(walkBehaviour)} component!");
        }

        if (jumpBehaviour == null)
        {
            Debug.LogError($"{name}: {nameof(jumpBehaviour)} is null!" +
                           $"\nThis class is dependant on a {nameof(jumpBehaviour)} component!");
        }
    }

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (walkBehaviour != null)
            walkBehaviour.Move(moveDirection);
    }

    public void HandleMoveInputCanceled(InputAction.CallbackContext context)
    {
        walkBehaviour.Move(Vector3.zero);
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (jumpBehaviour != null && context.started || context.performed)    
            jumpBehaviour.Jump();        
    }

    private void OnDisable()
    {
        moveAction.performed -= HandleMoveInput; 
        moveAction.canceled -= HandleMoveInputCanceled;

        jumpAction.performed -= HandleJumpInput;
    }
}