using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
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
        Vector3 _spawnPoint = _packet.ReadVector3();
        GameManager.instance.SpawnPlayer(_id, _username, _selectedCharacter, _spawnPoint, Quaternion.identity);
    }

    public static void PlayerPosition(Packet _packet)
    {
        float time = _packet.ReadFloat();
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        if(time > GameManager.instance.players[_id].lastPacketTime)
        {
            GameManager.instance.players[_id].lastPacketTime = time;
            GameManager.instance.players[_id].transform.position = _position;
        }
    }

 

    public static void PlayerAnimation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        bool[] _animationValues = new bool[_packet.ReadInt()];
        for (int i = 0; i < _animationValues.Length; i++)
        {
            _animationValues[i] = _packet.ReadBool();
        }
        GameManager.instance.players[_id].SetAnimations(_animationValues);

    }
    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();
        GameManager.instance.players[_id].transform.rotation = _rotation;
    }

    public static void Projectile(Packet _packet)
    {
        int _id = _packet.ReadInt();
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
    public static void SetInvis(Packet _packet)
    {
        Debug.Log("setting invis");
        int _id = _packet.ReadInt();
        bool _invis = _packet.ReadBool();
        GameManager.instance.players[_id].Invisible(_invis);
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
        GameManager.instance.players[Client.instance.myId].UpdateHUD(health, shield);
    }
    public static void UpdatePlayerCount(Packet _packet)
    {
        int alive = _packet.ReadInt();
        GameManager.instance.players[Client.instance.myId].UpdatePlayerCount(alive);
    }
    public static void ReceiveWinScreen(Packet _packet)
    {
        GameManager.instance.players[Client.instance.myId].Screen(1);
    }
    public static void ReceiveDeathScreen(Packet _packet)
    {
        int id = _packet.ReadInt();
        if (id == Client.instance.myId)
        {
            GameManager.instance.players[Client.instance.myId].Screen(0);
        }
        else
        {
            GameManager.instance.players[id].Die();
        }
    }
    public static void Reset(Packet _packet)
    {
        GameManager.instance.ResetGame();
        GameObject cam = (GameObject)Instantiate(Resources.Load("Main Camera"));
        cam.transform.position = new Vector3(12, -6, 20);
        cam.name = "Main Camera";
        UIManager.instance.ResetUI();
        Debug.Log("Set menu");
        UIManager.instance.setMenuStatus(true);
        UIManager.instance.LoadMenu(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public static void spawnItem(Packet _packet)
    {
        int key = _packet.ReadInt();
        int item = _packet.ReadInt();
        Vector3 pos = _packet.ReadVector3();
        GameObject prop = Instantiate(GameManager.instance.Items[item], pos, Quaternion.identity);
        gameItem thisItem = prop.GetComponentInChildren<gameItem>();
        thisItem.id = key;
        GameManager.instance.gameItems[key] = prop;
    }
    public static void Item(Packet _packet)
    {
        int id = _packet.ReadInt();
        int itemNumber = _packet.ReadInt();
        gameItem item = GameManager.instance.gameItems[id].GetComponentInChildren<gameItem>();
        if (itemNumber < 6)
        {
            inventory.instance.Add(item.item);
        }
        else
        {
            EquipmentInventory.instance.Add(item.item);
            VisualShield.instance.currentShield += 20;
            if (VisualShield.instance.currentShield >= 100)
            {
                VisualShield.instance.currentShield = 100;
            }
        }
    }
    public static void RemoveItem(Packet _packet)
    {
        int id = _packet.ReadInt();
        Destroy(GameManager.instance.gameItems[id]);
        GameManager.instance.gameItems[id] = null;
    }
    /*
      
                  PingReply reply = ping.Send(Client.instance.ip, 1000);
            Debug.Log(reply.RoundtripTime.ToString());
    */
}