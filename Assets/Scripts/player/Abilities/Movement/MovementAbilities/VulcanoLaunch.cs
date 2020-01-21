﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoLaunch : MovementBehaviour
{
    Animator animator;
    public float LaunchSpeed;
    public GameObject FreezeBone;
    public float timer = 5f;
    Vector3 StartRotation, StartPosition;

    protected override void Start()
    {
        if(Client.instance.host)
        animator = GetComponent<Animator>();
        animator.SetTrigger("Launch");
        StartRotation = FreezeBone.transform.eulerAngles;
        StartPosition = FreezeBone.transform.position;
    }
    protected override void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Destroy(this.gameObject);
        }
        base.Update();
    }
    public void LateUpdate()
    {
        //FreezeBone.transform.eulerAngles = StartRotation;
        //FreezeBone.transform.position = StartPosition;
    }
    public void VulcanoJump()
    {
        if (Client.instance.host)
        {
            Server.clients[Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].owner].player.status.ySpeed = LaunchSpeed;
        }
    }
}

