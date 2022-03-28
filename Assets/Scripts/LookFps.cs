using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFps : MonoBehaviour
{
    public float mouseSence = 100f;
    public Transform playerBody;

    float xRotation = 0f;
       
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSence * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSence * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler( xRotation , 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
