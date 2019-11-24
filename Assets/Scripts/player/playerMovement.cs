using Photon.Pun;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    CharacterController controller;
    public float speed;
    public float jumpspeed;
    public float gravity;
    public float mouseSens;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public int lives = 100;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
        if (!PV.IsMine)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;
        if (isGrounded && velocity.y < 0)
        {
            float y = Input.GetAxis("Jump");
            if (y != 0)
            {
                velocity.y = y * jumpspeed;
            }
            else
            {
                velocity.y = -gravity;
            }
        }

        controller.Move(movement * speed * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    public void hit()
    {
        lives -= 20;
    }
}
