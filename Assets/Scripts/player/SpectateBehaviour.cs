using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectateBehaviour : MonoBehaviour
{
    protected float yRotation = 0f;
    Vector3 velocity;
    CharacterController controller;
    public float movementSpeed;
    public float friction;
    public Transform Specatator;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Jump");

        Vector3 movement = transform.right * x + transform.up * y + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(movement * 2 * movementSpeed * Time.deltaTime);

        }
        else
        {
            controller.Move(movement * movementSpeed * Time.deltaTime);
        }
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        Specatator.Rotate(Vector3.up * mouseX);
    }
}
