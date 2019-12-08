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
        if (PV.IsMine)
        {
            PV.RPC("ChangeValues", RpcTarget.AllBuffered, new object[] { PhotonNetwork.LocalPlayer.NickName, Screen.width, Screen.height });
        }
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
    [PunRPC]
    void ChangeValues(string nick, int width, int height)
    {
        nickname.text = nick;
        widthDiff = (Screen.width - width) / 2;
        heightDiff = (Screen.height - height) / 2;
    }
}
