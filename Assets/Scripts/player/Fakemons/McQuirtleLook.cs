using Photon.Pun;
using System.IO;
using UnityEngine;

public class McQuirtleLook : playerLook
{
    public McQuirtleLook()
    {
        attackSpeed = 0;
        ATTACKSPEED = 0.2f;
        eAbility = 0;
        EABILITY = 10f;
    }
    protected override void Update()
    {
        base.Update();
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        camera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
    }
    protected override void basicAttack()
    {
        animator.SetTrigger("Attack");
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FireProjectile"), projectileSpawner.position, transform.rotation * Quaternion.Euler(yRotation,-18,0));
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
}
