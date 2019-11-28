using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    PhotonView PV;
    public float speed;
    Rigidbody controller;
    public float maxDistance;
    public float radius;
    public LayerMask groundMask;
    public LayerMask playerMask;
    public float damage;
    public string type;
    bool fired = false;
    // Start is called before the first frame update
    protected void Start()
    {
        PV = GetComponent<PhotonView>();
        controller = GetComponent<Rigidbody>();
        controller.velocity = transform.forward * speed * Time.deltaTime;
    }

    // Update is called once per frame
    protected void Update()
    {
        maxDistance -= speed * Time.deltaTime;
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
            if (hit.gameObject.layer == LayerMask.NameToLayer("ObjectWithLives"))
            {
                GameObject hitObject = hit.gameObject;
                Debug.Log(hitObject.name);
                playerBehavior actionScript = hitObject.GetComponent<playerBehavior>();
                actionScript.hit(damage, type);
            }
        }
        fired = true;
    }
}
