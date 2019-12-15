﻿using Photon.Pun;
using System.IO;
using UnityEngine;

public class VulcasaurLook : playerLook, IPunObservable
{
    public Vector3 projectileSpawnerLocalRotation;
    public Vector3 projectileSpawnerRotationEuler;
    VulcasaurLook()
    {
        attackSpeed = 0;
        ATTACKSPEED = 0.8f;
        eAbility = 0;
        EABILITY = 12f;
        qAbility = 0;
        QABILITY = 12f;
    }
    protected override void LateUpdate()
    {
        Head.localRotation = Quaternion.Euler(-1.14f, 17.087f, yRotation);
        avatarcamera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        base.LateUpdate();
    }
    protected override void basicAttack()
    {
        animator.SetTrigger("Attack");
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FireProjectile"), avatarcamera.position, avatarcamera.rotation);
        bullet.transform.name += 'b';
        attackSpeed = ATTACKSPEED;
    }
    protected override void eAttack()
    {
        Quaternion angleAdjust = Quaternion.Euler(16.622f, localTrans.rotation.y, 0);
        Vector3 positionAdjust = localTrans.up * 4f - localTrans.forward * -1;
        GameObject Vulcano = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Vulcano"), localTrans.position - positionAdjust, angleAdjust);
        Vulcano.transform.name += '*';
        Vulcano.transform.parent = avatar.transform;
        eAbility = EABILITY;
    }
    protected override void qAttack()
    {
        GameObject MagmaVines = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MagmaVines"), localTrans.position, localTrans.rotation);
        MagmaVines.transform.parent = localTrans;
        qAbility = QABILITY;
    }
    protected override void evolve()
    {
        base.evolve();
        avatar.GetComponentInChildren<Vulcasaur>().gravity = 0;
    }
    protected void evolve2()
    {
        PhotonNetwork.Destroy(avatar);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CharmandolphinAvatar"), evolveBulb.transform.position, evolveBulb.transform.rotation);
        PhotonNetwork.Destroy(evolveBulb);
    }
    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        base.OnPhotonSerializeView(stream, info);
    }
}
