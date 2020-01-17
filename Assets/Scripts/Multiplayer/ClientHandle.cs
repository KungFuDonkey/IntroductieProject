using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    static System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
    [SerializeField]static UnityEngine.Object mainCamera;
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
        float time = _packet.ReadFloat();
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        if(time > GameManager.players[_id].lastPacketTime)
        {
            GameManager.players[_id].lastPacketTime = time;
            PingReply reply = ping.Send(Client.instance.ip, 1000);
            Debug.Log(reply.RoundtripTime.ToString());
            GameManager.players[_id].transform.position = _position;
        }
    }

    public static void UseItem(Packet _packet)
    {
        Debug.Log("bahbah");
        int _id = _packet.ReadInt();
        int _itemIndex = _packet.ReadInt();
        //GameManager.players[_id].playert.UseItem(_packet.ReadInt());
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
        Quaternion headRotation = Quaternion.Euler(_packet.ReadFloat(), _rotation.y, _rotation.z);
        GameManager.players[_id].transform.rotation = _rotation;
        GameManager.players[_id].head.rotation = headRotation;
    }

    public static void Projectile(Packet _packet)
    {
        int _id = _packet.ReadInt();
        int something = _packet.ReadInt();
        Debug.Log(something);
        Vector3 _position = _packet.ReadVector3();
        int owner = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();
        int moveIndex = _packet.ReadInt();
        GameManager.instance.SpawnProjectile(_id, _position, _rotation, moveIndex, owner);
    }

    public static void ProjectileMove(Packet _packet)
    {
        float time = _packet.ReadFloat();
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        if(time > GameManager.projectiles[_id].lastPacketTime)
        {
            ProjectileManager projectile = GameManager.projectiles[_id];
            projectile.lastPacketTime = time;
            projectile.transform.rotation = _rotation;
            projectile.transform.position = _position;
        }
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
        UIManager.instance.LoadMenu(menu);
    }

    public static void UsernameList(Packet _packet)
    {
        int playercount = _packet.ReadInt();
        string list = _packet.ReadString();
        bool start = _packet.ReadBool();
        UIManager.instance.SetPlayerCount(playercount);
        UIManager.instance.SetUsernameList(list);
    }

    public static void GetMousePosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector2 _position = _packet.ReadVector2();
        UIManager.instance.mousePointers[_id].ChangePosition(_position);
    }
    public static void SetWalls(Packet _packet)
    {
        for (int i = 0; i < 4; i++)
        {
            GameManager.instance.walls[i].transform.position = _packet.ReadVector3();
        }
    }
    public static void UpdateHUD(Packet _packet)
    {
        float health = _packet.ReadFloat();
        float shield = _packet.ReadFloat();
        GameManager.players[Client.instance.myId].UpdateHUD(health, shield);
    }
    public static void UpdatePlayerCount(Packet _packet)
    {
        int alive = _packet.ReadInt();
        Debug.Log(alive);
        GameManager.players[Client.instance.myId].UpdatePlayerCount(alive);
    }
    public static void RecieveWinScreen(Packet _packet)
    {
        GameManager.players[Client.instance.myId].Screen(1);
    }
    public static void RecieveDeathScreen(Packet _packet)
    {
        GameManager.players[Client.instance.myId].Screen(0);
    }
    public static void Reset(Packet _packet)
    {
        GameObject cam = (GameObject)Instantiate(mainCamera);
        cam.transform.position = new Vector3(12, -6, 20);
        UIManager.instance.setMenuStatus(true);
        UIManager.instance.LoadMenu(1);
    }
}