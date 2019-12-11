using Photon.Pun;
using System.IO;
using UnityEngine;

public class VulcasaurLook : playerLook
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
        base.LateUpdate();
        transform.localRotation = Quaternion.Euler(0f, 0f, yRotation);
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
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FireProjectile"), projectileSpawner.position, transform.rotation * Quaternion.Euler(yRotation,-90,0));
        bullet.transform.name += 'b';
        attackSpeed = ATTACKSPEED;
    }
    protected override void eAttack()
    {
        //Transform Vulcasaur = VulcasaurAvatar.transform.GetChild(0).transform;
        Quaternion angleAdjust = Quaternion.Euler(16.622f, avatarTrans.rotation.y, 0);
        Vector3 positionAdjust = avatarTrans.up * 4f - avatarTrans.forward * -1;
        GameObject Vulcano = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Vulcano"), avatarTrans.position - positionAdjust, angleAdjust);
        Vulcano.transform.name += '*';
        Vulcano.transform.parent = avatar.transform;
        eAbility = EABILITY;
    }
    protected override void qAttack()
    {
        avatarTrans = avatar.transform.GetChild(0).transform;
        GameObject MagmaVines = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MagmaVines"), avatarTrans.position, avatarTrans.rotation);
        MagmaVines.transform.parent = avatarTrans;
        qAbility = QABILITY;
    }
    protected override void evolve()
    {
        base.evolve();
        avatar.GetComponentInChildren<Vulcasaur>().gravity = 0;
        //VulcasaurAvatar.GetComponentInChildren<VulcasaurLook>().enabled = false;
    }
    protected void evolve2()
    {
        PhotonNetwork.Destroy(avatar);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CharmandolphinAvatar"), evolveBulb.transform.position, evolveBulb.transform.rotation);
        PhotonNetwork.Destroy(evolveBulb);
    }
}
