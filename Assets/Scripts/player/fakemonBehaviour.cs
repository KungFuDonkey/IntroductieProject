using Photon.Pun;
using System.IO;
using UnityEngine;

public class fakemonBehaviour : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update
    public PhotonView PV;
    bool alive = true;
    GameObject MyAvatar;
    CharacterController controller;
    protected Animator animator;
    public GameObject hud;
    public GameObject[] hideobjects;
    HUD myHUD;
    int deadPlayers = 0;
    int alivePlayers;
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

    public Transform playerbody;
    public Transform Head;
    public Transform avatarcamera;
    public Transform projectileSpawner;
    public GameObject evolveBulb;
    public GameObject avatar;
    public float yRotation = 0f;
    protected float basicAttackSpeed, BASICATTACKSPEED;
    protected float eAbility, EABILITY;
    protected float qAbility, QABILITY;
    protected float evolveXP = 0f, evolveXPNeeded = 1000f, xpGenerator = 200f, evolveTime = 3f;
    protected bool canEvolve = true, evolving = false, movement = true;
    protected Transform avatarTrans, localTrans;

    public GameObject basicProjectile;
    public GameObject eAttackObject;
    public GameObject qAttackObject;

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
        if (!PV.IsMine)
        {
            Destroy(avatarcamera.gameObject);
        }

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        MyAvatar = transform.parent.gameObject;
        hud.transform.parent = transform.parent;
        myHUD = hud.GetComponent<HUD>();
        myHUD.MiniMap.playerTransform = transform;
        myHUD.healthBar.maxHealth = (int)lives;
        myHUD.AlivePlayers.text = "" + PhotonNetwork.CurrentRoom.Players.Count;

        Cursor.lockState = CursorLockMode.Locked;
        animator = playerbody.GetComponent<Animator>();
        avatarTrans = avatar.transform;
        localTrans = avatarTrans.GetChild(0).transform;
    }
    private void Update()
    {
        if (alive && movement)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = transform.right * horizontal + transform.forward * vertical;

            if (isGrounded && Input.GetAxis("Jump") == 1)
            {
                velocity.y = jumpspeed;
            }
            else if (!isGrounded)
            {
                velocity.y -= gravity * Time.deltaTime;
            }
            controller.Move(velocity * Time.deltaTime);

            if (movement.x != 0 || movement.z != 0)
            {
                controller.Move(movement * movementSpeed * Time.deltaTime);
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRunning", false);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    controller.Move(movement * 0.5f * movementSpeed * Time.deltaTime);
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", true);
                }
            }
            else
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", false);
            }
        }
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
    protected virtual void LateUpdate()
    {
        if (PV.IsMine)
        {
            if (alive)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");
                yRotation -= mouseY;
                yRotation = Mathf.Clamp(yRotation, -90f, 90f);
                playerbody.Rotate(Vector3.up * mouseX);

                if (movement)
                {
                    if (basicAttackSpeed > 0)
                    {
                        basicAttackSpeed -= Time.deltaTime;
                    }
                    if (eAbility > 0)
                    {
                        eAbility -= Time.deltaTime;
                    }
                    if (qAbility > 0)
                    {
                        qAbility -= Time.deltaTime;
                    }
                    if (evolveXP < evolveXPNeeded)
                    {
                        evolveXP += (xpGenerator * Time.deltaTime);

                    }

                    if (Input.GetMouseButton(0) && basicAttackSpeed <= 0)
                    {
                        basicAttack();
                    }
                    else if (Input.GetKey(KeyCode.E) && eAbility <= 0)
                    {
                        eAttack();
                    }
                    else if (Input.GetKey(KeyCode.Q) && qAbility <= 0)
                    {
                        qAttack();
                    }
                    else if (Input.GetKey(KeyCode.V) && evolveXP >= evolveXPNeeded)
                    {
                        evolve();
                    }
                }
                if (evolving)
                {
                    evolveBulb.transform.localScale += new Vector3(2, 2, 2) * Time.deltaTime;
                    localTrans.Translate(0, Time.deltaTime, 0);
                    evolveBulb.transform.Translate(0, Time.deltaTime, 0);
                }
            }
        }
    }
    [PunRPC]
    public void hit(float damage, string type)
    {
        if(type == weaktype)
        {
            lives -= (float)(0.66 * damage);
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
        /*foreach (GameObject player in players)
        {
            if (player.alive)
            {
                alivePlayers += 1;
            }
        }
        myHUD.AlivePlayers.text = "" + alivePlayers;*/
        myHUD.AlivePlayers.text = "" + (PhotonNetwork.CurrentRoom.Players.Count - deadPlayers);
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
    protected virtual void basicAttack()
    {
    }
    protected virtual void eAttack()
    {
    }
    protected virtual void qAttack()
    {
    }
    protected virtual void evolve()
    {
        avatarTrans = localTrans;
        evolving = true;
        movement = false;
        canEvolve = false;
        //spawning a new gameobject and destroying the old one
        evolveBulb = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "evolveBulb"), avatarTrans.position + new Vector3(0, 1, 0), avatarTrans.rotation);
        Invoke("evolve2", evolveTime);
    }
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(yRotation);
        }
        else
        {
            yRotation = (float)stream.ReceiveNext();
        }
    }
}

