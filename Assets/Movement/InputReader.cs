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

    }
    private void Awake()
    {
        if (walkBehaviour == null)
        {
            Debug.LogError($"{name}: {nameof(walkBehaviour)} is null!" +
                           $"\nThis class is dependant on a {nameof(walkBehaviour)} component!");
        }
    }
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 dir = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(dir.x, 0, dir.y) * Time.deltaTime;
    }
}