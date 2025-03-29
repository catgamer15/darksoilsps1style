using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[SelectionBase]



public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;


    [Header("Look")]
    [SerializeField] private float mouseSensivity = 2f;
    [SerializeField] private Transform cameraPivot;
  
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

   
    void Update()
    {
        HandleLook();
    }
    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity;

        transform.Rotate(Vector3.up * mouseX);


        float verticalRotation = -mouseY;


        float currentCameraAngle = cameraPivot.localEulerAngles.x;
        float newAngle = currentCameraAngle + verticalRotation;


        if (newAngle < 180) newAngle -= 360;
        newAngle = Mathf.Clamp(newAngle, -90, 90);



        cameraPivot.localEulerAngles = new Vector3(newAngle, 0, 0);
    }

}
