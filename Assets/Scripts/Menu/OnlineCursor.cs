using UnityEngine;
using UnityEngine.UI;
public class OnlineCursor : MousePointer
{
    public Text nickname;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        transform.parent = GameObject.Find("DelayStartMenu").transform;
        //nickname.text = PV.Owner.NickName;
    }
    protected override void Update()
    {
        if (true)
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
