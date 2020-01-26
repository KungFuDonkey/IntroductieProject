using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Server
{
    public static int MaxPlayers { get; private set; }
    public static int Port { get; private set; }

    public static Dictionary<int, ServerClient> clients = new Dictionary<int, ServerClient>();
    public static Dictionary<int, Projectile> projectiles = new Dictionary<int, Projectile>();
    public delegate void PacketHandler(int _fromClient, Packet _packet);
    public static Dictionary<int, PacketHandler> packetHandlers;
    public static Dictionary<int, Func<int,string,int,Player>> characters;
    public static System.Random rand = new System.Random();
    public static Dictionary<int, Vector3> spawnPoints = new Dictionary<int, Vector3>();
    private static TcpListener tcpListener;
    private static UdpClient udpListener;
    public static bool joinable = true;

    public static void Start(int _maxPlayers, int _port)
    {
        MaxPlayers = _maxPlayers;
        Port = _port;

        Debug.Log("starting server...");
        ServerStart.instance.DebugServer("starting server...");
        InitializeServerData();
        tcpListener = new TcpListener(IPAddress.Any, Port);
        tcpListener.Start();
        tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);

        udpListener = new UdpClient(Port);
        udpListener.BeginReceive(UDPReceiveCallback, null);

        Debug.Log($"Server started on port {Port}.");
        ServerStart.instance.DebugServer($"Server started on port {Port}.");
    }

    private static void TCPConnectCallback(IAsyncResult _result)
    {
        TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
        tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
        Debug.Log($"Incoming connection from {_client.Client.RemoteEndPoint}...");
        //ServerStart.instance.DebugServer($"Incoming connection from {_client.Client.RemoteEndPoint}...");
        if (joinable)
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)
                {

                    clients[i].tcp.Connect(_client);
                    return;
                }
            }
        }
        else
        {
            Debug.Log($"{_client.Client.RemoteEndPoint} failed to connect: Server already in a game");
            ServerStart.instance.DebugServer($"{_client.Client.RemoteEndPoint} failed to connect: Server already in a game");
            return;
        }
        Debug.Log($"{_client.Client.RemoteEndPoint} failed to connect: Server full!");
        ServerStart.instance.DebugServer($"{_client.Client.RemoteEndPoint} failed to connect: Server full!");
    }

    private static void UDPReceiveCallback(IAsyncResult _result)
    {
        try
        {
            IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] _data = udpListener.EndReceive(_result, ref _clientEndPoint);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            if (_data.Length < 4)
            {
                return;
            }

            using (Packet _packet = new Packet(_data))
            {
                int _clientId = _packet.ReadInt();

                if (_clientId == 0)
                {
                    return;
                }

                if (clients[_clientId].udp.endPoint == null)
                {
                    clients[_clientId].udp.Connect(_clientEndPoint);
                    return;
                }

                if (clients[_clientId].udp.endPoint.ToString() == _clientEndPoint.ToString())
                {
                    clients[_clientId].udp.HandleData(_packet);
                }
            }
        }
        catch (Exception _ex)
        {
            Debug.Log($"Error receiving UDP data: {_ex}");
            ServerStart.instance.DebugServer($"Error receiving UDP data: {_ex}");
        }
    }
    public static void SendUDPData(IPEndPoint _clientEndPoint, Packet _packet)
    {
        try
        {
            if (_clientEndPoint != null)
            {
                udpListener.BeginSend(_packet.ToArray(), _packet.Length(), _clientEndPoint, null, null);
            }
        }
        catch (Exception _ex)
        {
            Debug.Log($"Error sending data to {_clientEndPoint} via UDP: {_ex}");
            ServerStart.instance.DebugServer($"Error sending data to {_clientEndPoint} via UDP: {_ex}");
        }
    }

    private static void InitializeServerData()
    {
        for (int i = 1; i <= MaxPlayers; i++)
        {
            clients.Add(i, new ServerClient(i));
        }

        packetHandlers = new Dictionary<int, PacketHandler>()
        { 
            { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
            { (int)ClientPackets.playerMovement, ServerHandle.PlayerMovement },
            { (int)ClientPackets.mousePosition, ServerHandle.MousePosition},
            { (int)ClientPackets.ChoosePlayer, ServerHandle.ChoosePlayer},
            { (int)ClientPackets.ready, ServerHandle.ChangeReady},
            { (int)ClientPackets.AddEffects, ServerHandle.AddEffects},
            { (int)ClientPackets.SetInvis, ServerHandle.setInvis },
        };
        characters = new Dictionary<int, Func<int, string, int, Player>>()
        {
            { 0, charmandolphin },
            { 1, mcQuitle },
            { 2, vulcasaur }
        };
        for (int i = 0; i <= MaxPlayers; i++)
        {
            for(int j = 0; j <= MaxPlayers; j++)
            {
                //spawnpoints 10 steps off the wall and 15 high
                spawnPoints.Add((MaxPlayers + 1) * i + j, new Vector3(i * (280 / MaxPlayers) - 290, 15, j * (280 / MaxPlayers) - 290));
                //spawnPoints.Add((MaxPlayers + 1) * i + j, new Vector3(0, 110, -350));
            }
        }
        Debug.Log("Initialized packets.");
        ServerStart.instance.DebugServer("Initialized packets.");
        for (int i = 0; i < 4; i++)
        {
            Walls.walls[i] = GameManager.instance.walls[i].transform;
            Walls.startingPos[i] = GameManager.instance.walls[i].transform.position;
        }
        BattleBus.Bus = GameManager.instance.BattleBus.transform;
    }

    public static Player charmandolphin(int id, string username, int selectedcharacter)
    {
        return new Charmandolphin(id, username, selectedcharacter);
    }
    public static Player mcQuitle(int id, string username, int selectedcharacter)
    {
        return new McQuirtle(id, username, selectedcharacter);
    }
    public static Player vulcasaur(int id, string username, int selectedcharacter)
    {
        return new Vulcasaur(id, username, selectedcharacter);
    }
}
