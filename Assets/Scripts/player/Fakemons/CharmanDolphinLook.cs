﻿using Photon.Pun;
using System.IO;
using UnityEngine;

public class CharmandolphinLook : playerLook
{
    public GameObject CharmandolphinAvatar;
    public CharmandolphinLook()
    {
        attackSpeed = 0;
        ATTACKSPEED = 0.5f;
        eAbility = 0;
        EABILITY = 12f;
        evolveXPNeeded = 1000f;
        canEvolve = true;
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        camera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
    }
    protected override void basicAttack()
    {
        animator.SetTrigger("Attack");
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "WaterProjectile"), projectileSpawner.position, transform.rotation);
        bullet.transform.name += 'b';
        attackSpeed = ATTACKSPEED;
    }
    protected override void eAttack()
    {
        GameObject aoe = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AOE"), head.position, Quaternion.identity);
        aoe.transform.name += '*';
        aoe.transform.parent = playerbody;
        eAbility = EABILITY;
    }
    protected override void evolve()
    {
        canEvolve = false; //boolean for testing
        //spawning a new gameobject and destroying the old one
        Transform Vulcasaur = CharmandolphinAvatar.transform.GetChild(0).transform;
        GameObject evolution = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "McQuirtle"), Vulcasaur.position + new Vector3(0, 1, 0), Vulcasaur.rotation);
        PhotonNetwork.Destroy(CharmandolphinAvatar);
    }
}
