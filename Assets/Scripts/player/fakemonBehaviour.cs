using Photon.Pun;
using UnityEngine;
using System.IO;

public class fakemonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public PhotonView PV;
    bool alive = true; 
    GameObject MyAvatar;
    CharacterController controller;
    private Animator animator;
    public GameObject hud;
    public GameObject[] hideobjects;
    HUD myHUD;
    int deadPlayers = 0;
    protected string type;
    protected string weaktype;
    protected string strongtype;
    public float movementSpeed;
    public float jumpspeed;
    protected float lives;
    public float gravity;
    public float mouseSens;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;

    protected float effectTimer;
    protected bool effected;

    public static fakemonBehaviour instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PV = GetComponent<PhotonView>();
        name = PV.OwnerActorNr.ToString();
        if(PV.IsMine)
        {
            hud = Instantiate(hud);
            foreach (GameObject obj in hideobjects)
            {
                obj.layer = 10;
            }
        }

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        MyAvatar = transform.parent.gameObject;
        hud.transform.parent = transform.parent;
        myHUD = hud.GetComponent<HUD>();
        myHUD.MiniMap.playerTransform = transform;
        myHUD.healthBar.maxHealth = (int)lives;
        myHUD.AlivePlayers.text = "" + PhotonNetwork.CurrentRoom.Players.Count;
    }
    private void Update()
    {
        if (alive)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = transform.right * horizontal + transform.forward * vertical;
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
            if (effected)
            {
                effectTimer -= Time.deltaTime;
                if (effectTimer <= 0)
                {
                    movementSpeed *= 2f;
                    Debug.Log("noSlow");
                    effected = false;
                }
            }
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
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("Die");
        }
        myHUD.healthBar.CurrentHealth = (int)lives;
        Debug.Log(lives);
    }
    public void AddSpeed(Vector3 Speed)
    {
        velocity = velocity + Speed;
    }
    public void Die()
    {
        alive = false;
        PV.RPC("RPC_Die", RpcTarget.AllBuffered);

        Debug.Log("You Died");
        //PhotonNetwork.Destroy(MyAvatar);
        myHUD.ShowDeathscreen();
    }

    [PunRPC]
    protected virtual void RPC_Die()
    {
        Debug.Log("Died");

        deadPlayers += 1;
        myHUD.AlivePlayers.text = "" +(PhotonNetwork.CurrentRoom.Players.Count - deadPlayers);
    }
    public float Lives
    {
        get { return lives; }
    }
    [PunRPC]
    protected virtual void Slow()
    {
        Debug.Log("Slow");
        effected = true;
        effectTimer = 5f;
        movementSpeed *= 0.5f;
    }
}
