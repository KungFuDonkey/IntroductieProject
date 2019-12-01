using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    public Transform playerbody;
    public Transform head;
    public Transform camera; 
    private Animator animator;
    public Transform projectileSpawner;
    float yRotation = 0f;
    
    public float mouseSens;
    public float fireTimer = 2, FIRETIMER = 2;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        animator = playerbody.GetComponent<Animator>();
        if (!PV.IsMine)
        {
            Destroy(camera.gameObject);
        }
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (PV.IsMine)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            yRotation -= mouseY;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
            playerbody.Rotate(Vector3.up * mouseX);
            if(fireTimer > 0)
            {
                fireTimer -= Time.deltaTime;
            }
            camera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);

            if (Input.GetMouseButton(0) && fireTimer <= 0)
            {
                animator.SetTrigger("Attack");
                GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FireProjectile"), projectileSpawner.position, transform.rotation);
                bullet.transform.name += 'b';
                fireTimer = FIRETIMER;
            }
            if(Input.GetKey(KeyCode.E)){
                GameObject aoe = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AOE"), head.position, Quaternion.identity);
                aoe.transform.name += '*';
                aoe.transform.parent = playerbody;
            }
        }
    }
}
