using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ProjectileBehavior : Ability
{
    protected Rigidbody controller;
    protected float speed;
    protected float maxDistance;
    protected float particleTimer;
    private bool fired = false;
    protected bool destroyed = false;
    private float firedTimer = 0.1f;
    private Vector3 spawnPos;
    private float distance;
    
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<Rigidbody>();
        spawnPos = controller.transform.position;
        controller.velocity = transform.forward * speed;
    }
    
    protected override void Update()
    {
        base.Update();
        distance = Vector3.Distance(spawnPos, controller.transform.position);
        if (maxDistance < distance && !destroyed)
        {
            destroy();
        }
        if(!fired && firedTimer > 0)
        {
            firedTimer -= Time.deltaTime;
            if(firedTimer < 0)
            {
                fired = true;
            }
        }
    }
    void OnTriggerEnter(Collider hit)
    {
        if (fired)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                return;
            }
            else if (hit.gameObject.layer == LayerMask.NameToLayer("ObjectWithLives"))
            {
                PhotonView hitObject = hit.gameObject.GetPhotonView();
                hitObject.RPC("hit", RpcTarget.AllBuffered, new object[] { damage, type });
                if (statusEffect != "none")
                {
                    if (statusEffect == "slow")
                    {
                        hitObject.RPC("Slow", RpcTarget.AllBuffered);
                    }
                }
            }
            destroy();
        }
        fired = true;
    }
    protected virtual void destroy()
    {
        if (destroyed)
        {
            Destroy(gameObject);
            return;
        }
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<VisualEffect>().Stop();
        destroyed = true;
        controller.velocity = Vector3.zero;
        Invoke("destroy", 3f);
    }
}
