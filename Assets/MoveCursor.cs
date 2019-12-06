using Photon.Pun;
using UnityEngine;

public class MoveCursor : MonoBehaviour
{
    PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        transform.parent = GameObject.Find("Canvas").transform;
        Cursor.visible = false;
        if (!PV.IsMine)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x + 12, Input.mousePosition.y - 26, Input.mousePosition.z);
    }
}
