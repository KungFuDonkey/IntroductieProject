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
        Debug.Log(maxDistance);
        if (maxDistance < 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider hit)
    {
        if (fired)
        {
            Destroy(this.gameObject);
            if (hit.gameObject.layer == LayerMask.NameToLayer("ThisPlayer") && name[name.Length - 1] != 'b')
            {
                GameObject hitObject = hit.gameObject;
                Debug.Log(hitObject.name);
                fakemonBehaviour actionScript = hitObject.GetComponent<fakemonBehaviour>();
                actionScript.hit(damage, type);
            }
        }
        fired = true;
    }
}
