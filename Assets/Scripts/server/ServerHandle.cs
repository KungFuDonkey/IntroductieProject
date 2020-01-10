﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    //handle the welcome message and character spawining
    public static void WelcomeReceived(int _fromClient, ServerPacket _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();
        int selectedCharacter = _packet.ReadInt();



        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        ServerStart.instance.DebugServer($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            ServerStart.instance.DebugServer($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        Server.clients[_fromClient].SendIntoGame(_username, selectedCharacter);

    }
    //hanle playermovement in the game
    public static void PlayerMovement(int _fromClient, ServerPacket _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        //UnityEngine.Debug.LogError($"recieved package in { stopwatches[_fromClient - 1].Elapsed.TotalMilliseconds}ms from { _fromClient}");
        Quaternion _rotation = _packet.ReadQuaternion();
        Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
    }
}