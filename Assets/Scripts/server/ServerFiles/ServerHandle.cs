using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    //handle the welcome message and character spawining
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        ServerStart.instance.DebugServer($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            ServerStart.instance.DebugServer($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        Server.clients[_fromClient].username = _username;
        Server.clients[_fromClient].connected = true;
        ServerSend.SendUsernameList();
    }

    public static void ChangeReady(int _fromClient, Packet _packet)
    {
        Server.clients[_fromClient].ready = !Server.clients[_fromClient].ready;
        ServerSend.SendUsernameList();
    }

    public static void MousePosition(int _fromClient, Packet _packet)
    {
        Server.clients[_fromClient].mousePosition = _packet.ReadVector2();
    }

    public static void ChoosePlayer(int _fromClient, Packet _packet)
    {
        Server.clients[_fromClient].selectedCharacter = _packet.ReadInt();
    }

    //handle playermovement in the game
    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        Quaternion _rotation = _packet.ReadQuaternion();
        float _verticalRotation = _packet.ReadFloat();
        Server.clients[_fromClient].player.SetInput(_inputs, _rotation, _verticalRotation);
    }

    public static void AddEffects(int _fromClient, Packet _packet)
    {
        int item = _packet.ReadInt();
        int key = Server.clients[_fromClient].player.status.effectcount;
        if(item == 1)
        {
            Server.clients[_fromClient].player.status.effects.Add(key,new JumpBoost(10f, 2f, 4,key));
        }
        else if (item == 2)
        {
            Server.clients[_fromClient].player.status.effects.Add(key,new Invisible(10f, false, 4, key));
        }
        else if (item == 3)
        {
            Server.clients[_fromClient].player.status.effects.Add(key,new SpeedBoost(10f, 5f, 4, key));
        }
        else if (item == 4)
        {
            Server.clients[_fromClient].player.status.effects.Add(key, new ShieldBoost(10f, 25f, 4, key));
        }
        else if (item == 5)
        {
            Server.clients[_fromClient].player.status.effects.Add(key, new HealthBoost(10f, 25f, 4, key));
        }
        else if (item == 8)
        {
            Server.clients[_fromClient].player.status.effects.Add(key, new ShieldBoost(10f, 30f, 4, key));
        }
        else if (item == 12)
        {
            Server.clients[_fromClient].player.status.effects.Add(key, new SpeedBoost(10f, 1.5f, 4, key));
        }
        Server.clients[_fromClient].player.status.effectcount++;
    }

    public static void pickupItem(int _fromClient, int id, int itemNumber)
    {
        if (GameManager.instance.gameItems[id] != null)
        {
            gameItem item = GameManager.instance.gameItems[id].GetComponentInChildren<gameItem>();
            if (item.pickup)
            {
                item.pickup = false;
                ServerSend.Item(id, itemNumber, _fromClient);
            }
        }
        else
        {
            ServerSend.RemoveItem(id);
        }
    }

    public static void setInvis(int _fromClient, Packet _packet)
    {
        bool invis = _packet.ReadBool();
        ServerSend.SetInvis(_fromClient, invis);
    }

    public static void Reset()
    {
        foreach(ServerClient client in Server.clients.Values)
        {
            if (client.connected)
            {
                client.player = null;
            }
        }
        Server.projectiles = new Dictionary<int, Projectile>();
        Server.joinable = true;
        ServerStart.started = false;
        Walls.Reset();
        ServerSend.Reset();
    }
}
