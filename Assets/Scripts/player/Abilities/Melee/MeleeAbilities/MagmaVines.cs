using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaVines : MeleeBehaviour
{
    public LayerMask playerMask;
    MagmaVines()
    {
        range = 2.5f;
        damage = 10;
        type = "normal";
    }
    Animator Vines;
    // Start is called before the first frame update
    protected override void Start()
    {
        Vines = GetComponent<Animator>();
        Vines.SetTrigger("VineAttack");
        playerMask = LayerMask.NameToLayer("ObjectWithLives");
        transform.rotation = transform.parent.rotation;
    }
    public void VineAttackEnd()
    {
        //PhotonNetwork.Destroy(gameObject);
    }
    public void onAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * range + transform.up, 2, playerMask);
        foreach(Collider c in colliders)
        {
            //PhotonView hitObject = c.gameObject.GetPhotonView();
            //hitObject.RPC("hit", RpcTarget.AllBuffered, new object[] { damage, type });
        }

    }
}
