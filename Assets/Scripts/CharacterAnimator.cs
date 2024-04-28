using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private string speedParameter = "currentSpeed";

    private void Update()
    {
        if (animator && rigidBody)
        {
            var horVelocity = rigidBody.velocity;
            horVelocity.y = 0;
            var speed = horVelocity.magnitude;
            animator.SetFloat(speedParameter, speed);
        }
        else if(animator == null)
            Debug.LogError($"{name}: The {nameof(animator)} field is not assigned");
        else if (rigidBody == null)
            Debug.LogError($"{name}: The {nameof(rigidBody)} field is not assigned");
    }
}
