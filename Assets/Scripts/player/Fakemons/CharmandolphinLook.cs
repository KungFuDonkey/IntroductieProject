using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmandolphinLook : playerLook
{
    public CharmandolphinLook()
    {
       
    }
    protected override void LateUpdate()
    {
        Head.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        avatarcamera.rotation = Quaternion.Euler(verticalRotation, playerbody.rotation.eulerAngles.y, playerbody.rotation.z);
        base.LateUpdate();

    }
}
