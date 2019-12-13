using Photon.Pun;
using UnityEngine;

public class fakemonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    bool alive = true; 
    GameObject MyAvatar;
    CharacterController controller;
    private Animator animator;
    protected string type;
    protected string weaktype;
    protected string strongtype;
    protected float movementSpeed;
    protected float jumpspeed;
    protected float lives;
    public float gravity;
    public float mouseSens;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;
    void Start()
    {
        MyAvatar = transform.parent.gameObject;
        PV = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (!PV.IsMine)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
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

            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(movement * 2 * movementSpeed * Time.deltaTime);
                if (movement.x != 0 || movement.z != 0)
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", true);
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", false);
                }
            }
            else
            {
                controller.Move(movement * movementSpeed * Time.deltaTime);
                if (movement.x != 0 || movement.z != 0)
                {
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsRunning", false);
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", false);
                }
            }

            velocity.y -= gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
    [PunRPC]
    public void hit(float damage, string type)
    {
        if(type == weaktype)
        {
            lives -= (float)(0.5 * damage);
        }
        else if(type == strongtype)
        {
            lives -= (float)(1.5 * damage);
        }
        else
        {
            lives -= damage;
        }

        if (lives <= 0)
        {
            animator.SetTrigger("Die");
        }
        Debug.Log(lives);
    }
    public void AddSpeed(Vector3 Speed)
    {
        velocity = velocity + Speed;
    }
    public void Die()
    {
        alive = false;
        Debug.Log("You Died");
        //PhotonNetwork.Destroy(MyAvatar);
        GameController.GS.DeathScreen(); 
    }
}
