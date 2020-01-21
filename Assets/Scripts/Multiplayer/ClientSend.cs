using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);
            _packet.Write(UIManager.instance.selectedCharacter);

            SendTCPData(_packet);
        }
    }

    public static void AddEffects(int _item)
    {
        using (Packet _packet = new Packet((int)ClientPackets.AddEffects))
        {
            Debug.Log("ClientSend Effects");
            _packet.Write(_item);
            SendTCPData(_packet);
        }
    }

    public static void ChoosePlayer(int _character)
    {
        using (Packet _packet = new Packet((int)ClientPackets.ChoosePlayer))
        {
            _packet.Write(_character);
            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);
            _packet.Write(GameManager.players[Client.instance.myId].GetComponentInChildren<playerLook>().verticalRotation);
            SendUDPData(_packet);
        }
    }

    public static void Ready()
    {
        using (Packet _packet = new Packet((int)ClientPackets.ready))
        {
            SendTCPData(_packet);
        }
    }

    public static void MousePosition(Vector2 _position)
    {
        using (Packet _packet = new Packet((int)ClientPackets.mousePosition))
        {
            _packet.Write(_position);
            SendUDPData(_packet);
        }
    }

    public static void pickupItem(int id, int itemNumber)
    {
        using(Packet _packet = new Packet((int)ClientPackets.pickupItem))
        {
            _packet.Write(id);
            _packet.Write(itemNumber);
            SendTCPData(_packet);
        }
    }
    #endregion
}