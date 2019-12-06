using Photon.Pun;
using System.IO;
using UnityEngine;

public class VulcanoSaurLook : playerLook
{
    public GameObject VulcasaurAvatar; 
    VulcanoSaurLook()
    {
        attackSpeed = 0;
        ATTACKSPEED = 0.8f;
        eAbility = 0;
        EABILITY = 12f;
    }
    protected override void basicAttack()
    {
        animator.SetTrigger("Attack");
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FireProjectile"), projectileSpawner.position, transform.rotation);
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
        
    }
}
