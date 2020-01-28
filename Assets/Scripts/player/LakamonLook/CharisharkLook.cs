using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharisharkLook : playerLook
{
    public CharisharkLook()
    {
    }

    protected override void LateUpdate()
    {
        Head.localRotation = Quaternion.Euler(0f, verticalRotation, 0f);
        avatarcamera.rotation = Quaternion.Euler(verticalRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        base.LateUpdate();
    }
}
