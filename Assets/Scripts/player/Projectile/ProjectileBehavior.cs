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
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        controller = GetComponent<Rigidbody>();
        controller.velocity = transform.forward * speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        maxDistance -= speed * Time.deltaTime;
        if (maxDistance < 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        Destroy(this.gameObject);
        if (other.gameObject.tag == "Avatar")
        {
            other.gameObject.SendMessage("hit");
            Debug.Log("hit Avatar");
        }
    }
}
