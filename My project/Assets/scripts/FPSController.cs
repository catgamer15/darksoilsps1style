using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[SelectionBase]



public class FPSController : MonoBehaviour
{
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

        Debug.Log(mouseX + " " + mouseY);
    }
}
