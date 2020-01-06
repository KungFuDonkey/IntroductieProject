using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    public PlayerManager player;
    public float sensitivity = 100f;
    public float clampAngle = 85f;

    protected float verticalRotation;
    protected float horizontalRotation;

    public Transform playerbody;
    public Transform body;
    public Transform Head;
    public Transform avatarcamera;
    protected Animator animator;
    public Transform projectileSpawner;
    public GameObject avatar;

    protected Transform avatarTrans, localTrans;

    // Start is called before the first frame update
    void Start()
    {
        verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = player.transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        animator = body.GetComponent<Animator>();
        avatarTrans = avatar.transform;
        localTrans = avatarTrans.GetChild(0).transform;
    }



        
    
// Update is called once per frame
protected virtual void LateUpdate()
    {
        

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        verticalRotation += mouseY * sensitivity * Time.deltaTime;
        horizontalRotation += mouseX * sensitivity * Time.deltaTime;
        playerbody.localRotation = Quaternion.Euler(0f, horizontalRotation, 0f);
    }

}





