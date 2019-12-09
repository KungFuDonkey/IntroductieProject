using Photon.Pun;
using System.IO;
using UnityEngine;

public class McSquirtleLook : playerLook
{
    public GameObject burger;
    public float SpinDuration = 10f; 
    bool spinning = false;
    public McSquirtleLook()
    {
        attackSpeed = 0;
        ATTACKSPEED = 0.2f;
        eAbility = 0;
        EABILITY = 10f;
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        camera.rotation = Quaternion.Euler(yRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        if (spinning)
        {
            burger.transform.Rotate(Vector3.forward * 5);
            SpinDuration -= Time.deltaTime;
            if (SpinDuration <= 0)
            {
                animator.SetBool("InShellSpin", false);
                spinning = false;
            }
        }
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
        GameObject aoe = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "AOE"), head.position, Quaternion.identity);
        aoe.transform.name += '*';
        aoe.transform.parent = playerbody;
        eAbility = EABILITY;
    }
    protected override void qAttack()
    {
        base.qAttack();
        animator.SetBool("InShellSpin", true);
        spinning = true;
    }
}
