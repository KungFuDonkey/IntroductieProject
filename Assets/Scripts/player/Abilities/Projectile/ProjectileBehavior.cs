using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : Ability
{
    protected Rigidbody controller;
    protected float speed;
    protected float maxDistance;
    private bool fired = false;
    private float firedTimer = 0.1f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<Rigidbody>();
        controller.velocity = transform.forward * speed;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        maxDistance -= speed;
        if (maxDistance < 0)
        {
            PhotonNetwork.Destroy(this.gameObject);
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
            if (hit.gameObject.layer == LayerMask.NameToLayer("ThisPlayer") && name[name.Length - 1] != 'b')
            {
                PhotonView hitObject = hit.gameObject.GetPhotonView();
                hitObject.RPC("hit", RpcTarget.All, new object[] { damage, type });
            }
            PhotonNetwork.Destroy(this.gameObject);
        }
        fired = true;
    }
}
