using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using UnityEngine;

public class ServerClient
{
    public static int dataBufferSize = 4096;

    public int id;
    public Player player;
    public TCP tcp;
    public UDP udp;

    public ServerClient(int _clientId)
    {
        id = _clientId;
        tcp = new TCP(id);
        udp = new UDP(id);
    }

    public class TCP
    {
        public TcpClient socket;

        private readonly int id;
        private NetworkStream stream;
        private ServerPacket receivedData;
        private byte[] receiveBuffer;

        public TCP(int _id)
        {
            id = _id;
        }

        public void Connect(TcpClient _socket)
        {
            socket = _socket;
            socket.ReceiveBufferSize = dataBufferSize;
            socket.SendBufferSize = dataBufferSize;

            stream = socket.GetStream();

            receivedData = new ServerPacket();
            receiveBuffer = new byte[dataBufferSize];

            stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            ServerSend.Welcome(id, "Welcome to the server!");
        }

        public void SendData(ServerPacket _packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to player {id} via TCP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult _result)
        {
            //try and catch so the server doesn't crash when something is wrong
            try
            {
                //check if the player is still active
                int _byteLength = stream.EndRead(_result);
                if (_byteLength <= 0)
                {
                    // TODO: disconnect
                    return;
                }
                //copy the data to a new byteArray and prepare that array for reading
                byte[] _data = new byte[_byteLength];
                Array.Copy(receiveBuffer, _data, _byteLength);
                //handle the data and check if it can be reset
                receivedData.Reset(HandleData(_data));
                //start reading for the next variable
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error receiving TCP data: {_ex}");
                // TODO: disconnect
            }
        }

        private bool HandleData(byte[] _data)
        {
            int _packetLength = 0;
            receivedData.SetBytes(_data);

            //beginning of new packet reading the size of the packet if it was an int it is done reading
            if (receivedData.UnreadLength() >= 4)
            {
                _packetLength = receivedData.ReadInt();
                if (_packetLength <= 0)
                {
                    return true;
                }
            }
            
            //while the program isn't done reading the packet it continues reading
            while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
            {
                byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                //read on a different thread to save processing power on the main thread
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (ServerPacket _packet = new ServerPacket(_packetBytes))
                    {
                        //find out what kind of packet it is and send it to the packet handlers
                        int _packetId = _packet.ReadInt();
                        Server.packetHandlers[_packetId](id, _packet);
                    }
                });
                //check for the next variable and check if that maybe an int
                _packetLength = 0;
                if (receivedData.UnreadLength() >= 4)
                {
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }
            }
            //check if the packet is done reading if not the variable must be saved for the next HandleData
            if (_packetLength <= 1)
            {
                return true;
            }

            return false;
        }
    }

    public class UDP
    {
        public IPEndPoint endPoint;
        private int id;

        public UDP(int _id)
        {
            id = _id;
        }
        //setup the connection
        public void Connect(IPEndPoint _endPoint)
        {
            endPoint = _endPoint;
        }
        //send a packet over the network via udp
        public void SendData(ServerPacket _packet)
        {
            Server.SendUDPData(endPoint, _packet);
        }
        //handle the data of the packet
        public void HandleData(ServerPacket _packetData)
        {
            int _packetLength = _packetData.ReadInt();
            byte[] _packetBytes = _packetData.ReadBytes(_packetLength);

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (ServerPacket _packet = new ServerPacket(_packetBytes))
                {
                    int _packetId = _packet.ReadInt();
                    Server.packetHandlers[_packetId](id, _packet);
                }
            });
        }
    }
    //send the player an action to spawn a certain character and for the new joining player to spawn all other characters
    public void SendIntoGame(string _playerName, int _selectedCharacter)
    {
        player = new Player(id, _playerName, Vector3.zero, _selectedCharacter);

        foreach (ServerClient _client in Server.clients.Values)
        {
            if (_client.player != null)
            {
                if (_client.id != id)
                {
                    ServerSend.SpawnPlayer(id, _client.player);
                }
            }
        }

        foreach (ServerClient _client in Server.clients.Values)
        {
            if (_client.player != null)
            {
                ServerSend.SpawnPlayer(_client.id, player);
            }
        }
    }

    
}
