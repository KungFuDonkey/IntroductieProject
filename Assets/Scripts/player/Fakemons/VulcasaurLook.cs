using Photon.Pun;
using System.IO;
using UnityEngine;

public class VulcasaurLook : fakemonBehaviour, IPunObservable
{
    public Vector3 projectileSpawnerLocalRotation;
    public Vector3 projectileSpawnerRotationEuler;
    VulcasaurLook()
    {
        basicAttackSpeed = 0;
        BASICATTACKSPEED = 0.8f;
        eAbility = 0;
        EABILITY = 12f;
        qAbility = 0;
        QABILITY = 12f;
        type = "fire";
        movementSpeed = 7;
        jumpspeed = 10;
        lives = 100;
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
        GameObject bullet = Instantiate(basicProjectile, projectileSpawner.position, avatarcamera.rotation);
        bullet.transform.name += 'b';
        basicAttackSpeed = BASICATTACKSPEED;
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

    [PunRPC]
    protected override void RPC_Die()
    {
        base.RPC_Die();
    }
}
