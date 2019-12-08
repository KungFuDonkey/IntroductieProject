using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class OnlineCursor : MousePointer
{
    PhotonView PV;
    public Text nickname;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PV = GetComponent<PhotonView>();
        transform.parent = GameObject.Find("DelayStartMenu").transform;
        nickname.text = PV.Owner.NickName;
    }
    protected override void Update()
    {
        if (PV.IsMine)
        {
            base.Update();
        }
    }
    /*
    private void LateUpdate()
    {
        if (!PV.IsMine)
        {
            transform.position += Vector3.right * widthDiff + Vector3.up * heightDiff;
        }
    }
    */
}
