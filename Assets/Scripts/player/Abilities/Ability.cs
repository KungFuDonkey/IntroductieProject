using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected PhotonView PV;
    protected float damage;
    protected string type;
    protected string statusEffect = "none";
    // Start is called before the first frame update
    protected virtual void Start()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
