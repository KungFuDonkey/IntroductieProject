using Photon.Pun;
using System.IO;
using UnityEngine;

public class CharmandolphinLook : playerLook
{
    public CharmandolphinLook()
    {
        attackSpeed = 0;
        ATTACKSPEED = 0.5f;
        eAbility = 0;
        EABILITY = 12f;
        evolveXPNeeded = 1000f;
        evolveXP = 0f;
        canEvolve = true;
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        camera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        if (hover)
        {
            avatarTrans.Translate(0, Time.deltaTime, 0);
            evolveBulb.transform.Translate(0, Time.deltaTime, 0);
        }
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
        base.evolve();
        avatar.GetComponentInChildren<Charmandolphin>().gravity = 0;
        //CharmandolphinAvatar.GetComponentInChildren<CharmandolphinLook>().enabled = false;
    }
    protected void evolve2()
    {
        PhotonNetwork.Destroy(avatar);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "McQuirtleAvatar"), evolveBulb.transform.position, evolveBulb.transform.rotation);
        PhotonNetwork.Destroy(evolveBulb);
    }
}
