using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    PhotonView PV;
    protected float damage;
    protected string type;
    public LayerMask playerMask;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
