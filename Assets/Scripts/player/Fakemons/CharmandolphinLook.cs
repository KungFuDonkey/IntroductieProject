using Photon.Pun;
using System.IO;
using UnityEngine;

public class CharmandolphinLook : fakemonBehaviour, IPunObservable
{
    public CharmandolphinLook()
    {
        basicAttackSpeed = 0;
        BASICATTACKSPEED = 0.5f;
        eAbility = 0;
        EABILITY = 12f;
        qAbility = 0;
        QABILITY = 12f;
        type = "water";
        movementSpeed = 10;
        jumpspeed = 10;
        lives = 100;
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
        GameObject bullet = Instantiate(basicProjectile, projectileSpawner.position, avatarcamera.rotation);
        bullet.transform.name += 'b';
        basicAttackSpeed = BASICATTACKSPEED;
    }

    protected override void eAttack()
    {
        GameObject aoe = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AOE"), transform.position, Quaternion.identity);
        aoe.transform.name += '*';
        aoe.transform.parent = playerbody;
        eAbility = EABILITY;
    }

    protected override void qAttack()
    {
        animator.SetTrigger("Attack");
        GameObject aquaRing = Instantiate(qAttackObject, projectileSpawner.position, avatarcamera.rotation);
        aquaRing.transform.name += 'b';
        qAbility = QABILITY;
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

    [PunRPC]
    protected override void RPC_Die()
    {
        base.RPC_Die();
    }
}
