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
    public float damageDelay = 0.2f;
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
        if(damageDelay >= 0)
        {
            damageDelay -= Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider hit)
    {
        Debug.Log(damageDelay);
        if (damageDelay < 0)
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
    }
}
