using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PompeisaurLook : playerLook
{
    PompeisaurLook()
    {
    }

    protected override void LateUpdate()
    {
        Head.localRotation = Quaternion.Euler(0, 0, verticalRotation);
        avatarcamera.rotation = Quaternion.Euler(verticalRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        base.LateUpdate();
    }
}