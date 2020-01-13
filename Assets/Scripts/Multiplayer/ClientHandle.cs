using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    static System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
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
        Vector3 _spawnPoint = _packet.ReadVector3();
        GameManager.instance.SpawnPlayer(_id, _username, _selectedCharacter, _spawnPoint, Quaternion.identity);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        PingReply reply = ping.Send(Client.instance.ip, 1000);
        Debug.Log(reply.RoundtripTime.ToString());
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
        int owner = _packet.ReadInt();
        GameManager.instance.SpawnProjectile(_id, _position, _rotation, projectile, owner);
    }

    public static void ProjectileMove(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.projectiles[_id].transform.rotation = _rotation;
        GameManager.projectiles[_id].transform.position = _position;
    }

    public static void ProjectileDestroy(Packet _packet)
    {
        int _id = _packet.ReadInt();
        GameManager.projectiles[_id].DestroyProjectile();
        GameManager.projectiles.Remove(_id);
    }

    public static void Damage(Packet _packet)
    {
        float damage = _packet.ReadFloat();
        int type = _packet.ReadInt();
        Debug.Log(damage);
    }

    public static void LoadMenu(Packet _packet)
    {
        int menu = _packet.ReadInt();
        Debug.Log(menu);
        UIManager.instance.LoadMenu(menu);
    }

    public static void UsernameList(Packet _packet)
    {
        int playercount = _packet.ReadInt();
        string list = _packet.ReadString();
        UIManager.instance.SetPlayerCount(playercount);
        UIManager.instance.SetUsernameList(list);
    }

    public static void GetMousePosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector2 _position = _packet.ReadVector2();
        UIManager.instance.mousePointers[_id].ChangePosition(_position);
    }
}