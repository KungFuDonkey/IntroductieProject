﻿using System.Collections;
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

    public static void UseItem(int _itemIndex)
    {
        using (Packet _packet = new Packet((int)ClientPackets.UseItem))
        {
            _packet.Write(_itemIndex);
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
            _packet.Write(GameManager.players[Client.instance.myId].GetComponent<playerLook>().verticalRotation);
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
    #endregion
}