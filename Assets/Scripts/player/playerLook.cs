using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    // Start is called before the first frame update
    protected PhotonView PV;
    public Transform playerbody;
    public Transform head;
    public Transform camera; 
    protected Animator animator;
    public Transform projectileSpawner;
    float yRotation = 0f;
    
    public float mouseSens;
    protected float attackSpeed, ATTACKSPEED;
    protected float eAbility, EABILITY;
    protected float qAbility, QABILITY;
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
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerbody.Rotate(Vector3.up * mouseX);
        if (attackSpeed > 0)
        {
            attackSpeed -= Time.deltaTime;
        }
        if (eAbility > 0)
        {
            eAbility -= Time.deltaTime;
        }
        camera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);

        if (Input.GetMouseButton(0) && attackSpeed <= 0)
        {
            basicAttack();
        }
        else if (Input.GetKey(KeyCode.E) && eAbility <= 0)
        {
            eAttack();
        }
        else if(Input.GetKey(KeyCode.Q) && qAbility <= 0)
        {

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
}
