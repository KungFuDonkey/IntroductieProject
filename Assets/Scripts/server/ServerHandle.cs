using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    //handle the welcome message and character spawining
    public static void WelcomeReceived(int _fromClient, ServerPacket _packet)
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

    public static void changeReady(int _fromClient, ServerPacket _packet)
    {
        Server.clients[_fromClient].ready = !Server.clients[_fromClient].ready;
        ServerSend.SendUsernameList();
    }
    public static void MousePosition(int _fromClient, ServerPacket _packet)
    {
        Server.clients[_fromClient].mousePosition = _packet.ReadVector2();
    }

    public static void ChoosePlayer(int _fromClient, ServerPacket _packet)
    {
        Server.clients[_fromClient].selectedCharacter = _packet.ReadInt();
    }
    //handle playermovement in the game
    public static void PlayerMovement(int _fromClient, ServerPacket _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        //UnityEngine.Debug.LogError($"recieved package in { stopwatches[_fromClient - 1].Elapsed.TotalMilliseconds}ms from { _fromClient}");
        Quaternion _rotation = _packet.ReadQuaternion();
        float _verticalRotation = _packet.ReadFloat();
        Server.clients[_fromClient].player.SetInput(_inputs, _rotation, _verticalRotation);
    }

    public static void UseItem(int _fromClient, ServerPacket _packet)
    {
        Debug.Log("bahbah");
        Server.clients[_fromClient].player.UseItem(_packet.ReadInt());
    }

}
