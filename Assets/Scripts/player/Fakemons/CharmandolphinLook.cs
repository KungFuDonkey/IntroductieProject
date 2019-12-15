﻿using Photon.Pun;
using System.IO;
using UnityEngine;

public class CharmandolphinLook : playerLook, IPunObservable
{
    public CharmandolphinLook()
    {
        attackSpeed = 0;
        ATTACKSPEED = 0.5f;
        eAbility = 0;
        EABILITY = 12f;
    }
    protected override void LateUpdate()
    {
        Head.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        avatarcamera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        base.LateUpdate();

    }
    protected override void basicAttack()
    {
        animator.SetTrigger("Attack");
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "WaterProjectile"), avatarcamera.position, avatarcamera.rotation);
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
        avatar.GetComponentInChildren<Charmandolphin>().gravity = 0;
    }
    protected void evolve2()
    {
        PhotonNetwork.Destroy(avatar);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "McQuirtleAvatar"), evolveBulb.transform.position, evolveBulb.transform.rotation);
        PhotonNetwork.Destroy(evolveBulb);
    }
    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        base.OnPhotonSerializeView(stream, info);
    }
}
