using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoLaunch : MovementBehaviour
{
    Animator animator;
    public float LaunchSpeed;
    public GameObject FreezeBone;
    Vector3 StartRotation, StartPosition;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Launch");
        StartRotation = FreezeBone.transform.eulerAngles;
        StartPosition = FreezeBone.transform.position;
    }

    public void LateUpdate()
    {
        FreezeBone.transform.eulerAngles = StartRotation;
        FreezeBone.transform.position = StartPosition;
    }
    public void VulcanoJump()
    {
        fakemonBehaviour fakemonBehaviour = transform.parent.GetComponentInChildren<Vulcasaur>();
        fakemonBehaviour.AddSpeed(new Vector3(0, LaunchSpeed, 0));
    }
}
