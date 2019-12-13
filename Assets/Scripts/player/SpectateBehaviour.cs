using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectateBehaviour : MonoBehaviour
{
    protected float yRotation = 0f;
    Vector3 velocity;
    CharacterController controller;
    protected float movementSpeed;
    public float friction;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(movement * 2 * movementSpeed * Time.deltaTime);

        }
        else
        {
            controller.Move(movement * movementSpeed * Time.deltaTime);
        }

        float y = Input.GetAxis("Jump");
        if (velocity.y < 0)
        {
            velocity.y += friction * Time.deltaTime;

        }
        else
        {
            velocity.y -= friction * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        yRotation -= mouseY;


        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);





    }
}
