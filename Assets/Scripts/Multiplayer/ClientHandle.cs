using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect();
    }


    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        int _selectedCharacter = _packet.ReadInt();

        GameManager.instance.SpawnPlayer(_id, _username, _selectedCharacter, Vector3.zero, Quaternion.identity);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Debug.Log(float.Parse(DateTime.Now.ToString("ss.fff")) - float.Parse(_packet.ReadString()));
        GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerAnimation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool[] _animationValues = new bool[_packet.ReadInt()];
        for (int i = 0; i < _animationValues.Length; i++)
        {
            _animationValues[i] = _packet.ReadBool();
        }
        GameManager.players[_id].SetAnimations(_animationValues);

    }
    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void Projectile(Packet _packet)
    {
        int _id = _packet.ReadInt();
        int something = _packet.ReadInt();
        Debug.Log(something);
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        int projectile = _packet.ReadInt();

        GameManager.instance.SpawnProjectile(_id, _position, _rotation, projectile);
    }

    public static void ProjectileMove(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.projectiles[_id].transform.rotation = _rotation;
        GameManager.projectiles[_id].transform.position = _position;
    }
}