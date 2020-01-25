using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcasaurLook : playerLook
{
    VulcasaurLook()
    {
    }

    protected override void LateUpdate()
    {
        Head.localRotation = Quaternion.Euler(-1.14f, 17.087f, verticalRotation);
        avatarcamera.rotation = Quaternion.Euler(verticalRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        base.LateUpdate();
    }
}
