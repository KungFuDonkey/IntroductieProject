using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McQuirtleLook : playerLook
{
    public McQuirtleLook()
    {
    }

    protected override void LateUpdate()
    {
        Head.localRotation = Quaternion.Euler(verticalRotation, 17.974f, 0f);
        avatarcamera.rotation = Quaternion.Euler(verticalRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        base.LateUpdate();
    }
}
