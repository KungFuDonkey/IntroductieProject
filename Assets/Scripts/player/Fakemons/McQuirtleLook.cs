﻿using Photon.Pun;
using System.IO;
using UnityEngine;

public class McQuirtleLook : playerLook, IPunObservable
{
    public McQuirtleLook()
    {
        attackSpeed = 0;
        ATTACKSPEED = 0.2f;
        eAbility = 0;
        EABILITY = 10f;
    }
    protected override void LateUpdate()
    {
        Head.localRotation = Quaternion.Euler(yRotation, 17.974f, 0f);
        avatarcamera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        base.LateUpdate();

    }
    protected override void basicAttack()
    {
        animator.SetTrigger("Attack");
        Rigidbody bullet = Instantiate(bulletProjectile, avatarcamera.position, avatarcamera.rotation);
        bullet.transform.name += 'b';
        attackSpeed = ATTACKSPEED;
    }
    protected override void eAttack()
    {
        GameObject aoe = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AOE"), transform.position, Quaternion.identity);
        aoe.transform.name += '*';
        aoe.transform.parent = playerbody;
        eAbility = EABILITY;
    }
    protected override void evolve()
    {
        base.evolve();
        avatar.GetComponentInChildren<McQuirtle>().gravity = 0;
    }
    protected void evolve2()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "VulcasaurAvatar"), localTrans.position, localTrans.rotation);
        PhotonNetwork.Destroy(avatar);
        PhotonNetwork.Destroy(evolveBulb);
    }
    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        base.OnPhotonSerializeView(stream, info);
    }
}
