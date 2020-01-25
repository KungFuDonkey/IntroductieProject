using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : Ability
{
    protected Rigidbody controller;
    protected float speed;
    protected float maxDistance;
    private bool fired = false;
    protected bool destroyed = false;
    private float firedTimer = 0.1f;

    protected override void Start()
    {
        controller = GetComponent<Rigidbody>();
        controller.velocity = transform.forward * speed;
    }

    protected override void Update()
    {
        maxDistance -= speed * Time.deltaTime;
        if (maxDistance < 0 && !destroyed)
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
            if (hit.gameObject.layer == LayerMask.NameToLayer("ObjectWithLives"))
            {
                /*PhotonView hitObject = hit.gameObject.GetPhotonView();
                hitObject.RPC("hit", RpcTarget.AllBuffered, new object[] { damage, type, statusEffect });
                if (statusEffect != "none")
                {
                    if (statusEffect == "slow")
                    {
                        hitObject.RPC("Slow", RpcTarget.AllBuffered);
                    }
                }*/
            }
            destroy();
        }
        fired = true;
    }
    private void destroy()
    {
        Destroy(transform.GetChild(0).gameObject);
        destroyed = true;
        GameObject childObj = GameObject.Find("Sphere");
        //childObj.SetActive(false);
        transform.position = Vector3.zero;
        controller.velocity = Vector3.zero;
    }
}
