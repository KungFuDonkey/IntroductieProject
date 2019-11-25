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
    Vector3 latestRotation;
    Vector3 latestPosition;
    public float cameraShake = 1;
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
        latestRotation = head.localRotation.eulerAngles;
        latestPosition = head.localPosition;
        if (!PV.IsMine)
        {
            Destroy(camera.gameObject);
        }
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 newRotation = head.localRotation.eulerAngles;
        //Vector3 newPosition = head.localPosition;
        //Vector3 divPosition = newPosition - latestPosition;
        //Vector3 divRotation = newRotation - latestRotation;
        //transform.localPosition = latestRotation + cameraShake * divPosition;
        //transform.localRotation = Quaternion.Euler(latestRotation + cameraShake * divRotation);
        //latestRotation = head.localRotation.eulerAngles;
        //latestPosition = head.localPosition;
        if (PV.IsMine)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            yRotation -= mouseY;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
            playerbody.Rotate(Vector3.up * mouseX);
            fireTimer -= Time.deltaTime;
            if (Input.GetMouseButton(0) && fireTimer < 0)
            {
                animator.SetTrigger("Attack");
                GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FireProjectile"), projectileSpawner.position, transform.rotation);
                bullet.name = playerbody.name + "b";
                Debug.Log("Creating Bullet");
                fireTimer = FIRETIMER;
            }
        }
    }
}
