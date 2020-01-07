using Photon.Pun;
using System.IO;
using UnityEngine;

public class McQuirtleLook : fakemonBehaviour, IPunObservable
{
    public McQuirtleLook()
    {
        basicAttackSpeed = 0;
        BASICATTACKSPEED = 0.2f;
        eAbility = 0;
        EABILITY = 10f;
        qAbility = 0;
        QABILITY = 10f;
        type = "grass";
        movementSpeed = 12;
        jumpspeed = 14;
        lives = 100;
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
        GameObject bullet = Instantiate(basicProjectile, avatarcamera.position, avatarcamera.rotation, avatar.transform);
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
        GameObject grass = Instantiate(qAttackObject, avatarcamera.position + new Vector3(0, -5, 3), Quaternion.identity, avatar.transform);
        grass.transform.name += 'm';
        qAbility = QABILITY;
        Destroy(grass, 4f);
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

    [PunRPC]
    protected override void RPC_Die()
    {
        base.RPC_Die();
    }
}
