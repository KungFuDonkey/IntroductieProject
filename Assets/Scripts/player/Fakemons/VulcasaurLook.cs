using Photon.Pun;
using System.IO;
using UnityEngine;

public class VulcasaurLook : playerLook
{
    public GameObject VulcasaurAvatar;
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
        evolveXPNeeded = 0f;
        evolveXP = 0f;
        canEvolve = true;
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        transform.localRotation = Quaternion.Euler(0f, 0f, yRotation);
        camera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
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
        Transform Vulcasaur = VulcasaurAvatar.transform.GetChild(0).transform;
        Quaternion angleAdjust = Quaternion.Euler(16.622f, Vulcasaur.rotation.y, 0);
        Vector3 positionAdjust = Vulcasaur.up * 4f - Vulcasaur.forward * -1;
        GameObject Vulcano = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Vulcano"), Vulcasaur.position - positionAdjust, angleAdjust);
        Vulcano.transform.name += '*';
        Vulcano.transform.parent = VulcasaurAvatar.transform;
        eAbility = EABILITY;
    }
    protected override void qAttack()
    {
        Transform Vulcasaur = VulcasaurAvatar.transform.GetChild(0).transform;
        GameObject MagmaVines = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MagmaVines"), Vulcasaur.position, Vulcasaur.rotation);
        MagmaVines.transform.parent = Vulcasaur.transform;
        qAbility = QABILITY;
    }
    protected override void evolve()
    {
        Debug.Log("evolve");
        canEvolve = false; //boolean for testing
        //spawning a new gameobject and destroying the old one
        Transform Vulcasaur = VulcasaurAvatar.transform.GetChild(0).transform;
        GameObject evolution = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Charmandolphin"), Vulcasaur.position + new Vector3(0, 1, 0), Vulcasaur.rotation);
        PhotonNetwork.Destroy(VulcasaurAvatar);
    }
}
