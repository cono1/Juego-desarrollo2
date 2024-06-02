using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 positionOffset = new Vector3(0f, 1f, -5f);
    [SerializeField] private float smoothness = 5f;

    void Start()
    {
        if(player == null)
        {
            Debug.LogError($"{name}: The character is not assigned to the camera");
        }
    }

    private void LateUpdate()
    {
        RotateCamera();
        MoveCamera();
    }

    private void RotateCamera()
    {
        var desiredRotation = player.rotation * Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 5);

        transform.rotation = rotation;
        //transform.LookAt(player);
    }

    private void MoveCamera()
    {
        Vector3 forward = transform.rotation * Vector3.forward;
        Vector3 right = transform.rotation * Vector3.right;
        Vector3 up = Vector3.up;

        Vector3 targetPos = player.position;
        Vector3 desiredPosition = targetPos
                                + forward * positionOffset.z
                                + right * positionOffset.x
                                + up * positionOffset.y;

        Vector3 position = Vector3.Slerp(transform.position, desiredPosition, Time.deltaTime * smoothness);
        transform.position = position;

        Vector3 targetDir = player.transform.position;
        targetDir.y += positionOffset.y; 

        transform.LookAt(targetDir);
    }
}
