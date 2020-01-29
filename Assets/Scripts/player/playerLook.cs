using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    public PlayerManager player;
    public float sensitivity = 100f;
    public float clampAngle = 85f;
    public float verticalRotation;
    protected float horizontalRotation;

    public Transform playerbody;
    public Transform Head;
    public Transform avatarcamera;
    protected Animator animator;
    public Transform projectileSpawner;
    public GameObject avatar;

    protected Transform avatarTrans, localTrans;

    void Start()
    {
        verticalRotation = transform.localEulerAngles.x;
        horizontalRotation = player.transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        animator = playerbody.GetComponent<Animator>();
        avatarTrans = avatar.transform;
        localTrans = avatarTrans.GetChild(1).transform;
    }

    protected virtual void LateUpdate()
    {
        float mouseX;
        float mouseY;
        if (!GameManager.instance.freezeInput)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = -Input.GetAxis("Mouse Y");
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseX = 0;
            mouseY = 0;
        }
        if (!GameManager.instance.inInventory)
        {
            verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);
            verticalRotation += mouseY * sensitivity * Time.deltaTime;
            horizontalRotation += mouseX * sensitivity * Time.deltaTime;
            playerbody.localRotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        }
    }
}
