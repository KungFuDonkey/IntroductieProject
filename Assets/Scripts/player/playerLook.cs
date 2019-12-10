using Photon.Pun;
using System.IO;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    protected PhotonView PV;
    public Transform playerbody;
    public Transform head;
    public Transform camera;
    protected Animator animator;
    public Transform projectileSpawner;
    public GameObject evolveBulb;
    public GameObject avatar;
    protected float yRotation = 0f;


    public float mouseSens;
    protected float attackSpeed, ATTACKSPEED;
    protected float eAbility, EABILITY;
    protected float qAbility, QABILITY;
    protected float evolveXP, evolveXPNeeded, xpGenerator = 200f, evolveTime;
    protected bool canEvolve, hover;
    protected Transform avatarTrans;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        animator = playerbody.GetComponent<Animator>();
        if (!PV.IsMine)
        {
            Destroy(camera.gameObject);
            Destroy(this);
        }
        avatarTrans = avatar.transform;
    }

    // Update is called once per frame
    protected virtual void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        playerbody.Rotate(Vector3.up * mouseX);
        if (attackSpeed > 0)
        {
            attackSpeed -= Time.deltaTime;
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

        if (Input.GetMouseButton(0) && attackSpeed <= 0)
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
        else if (Input.GetKey(KeyCode.V) && evolveXP >= evolveXPNeeded && canEvolve)
        {
            evolve();
        }

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
        canEvolve = false;
        evolveTime = 3f;
        //spawning a new gameobject and destroying the old one
        evolveBulb = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "evolveBulb"), transform.position + new Vector3(0, 1, 0), transform.rotation);
        Invoke("evolve2", evolveTime);
        //add animation: in the air after jumping out of pokeball
        hover = true;
    }
}
