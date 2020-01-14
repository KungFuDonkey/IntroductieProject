using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServerSend 
{
    //functions for sending TCP or UDP data to different clients
    private static void SendTCPData(int _toClient, ServerPacket _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    private static void SendUDPData(int _toClient, ServerPacket _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    private static void SendTCPDataToAll(ServerPacket _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }
    private static void SendTCPDataToAll(int _exceptClient, ServerPacket _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
    }

    private static void SendUDPDataToAll(ServerPacket _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }
    private static void SendUDPDataToAll(int _exceptClient, ServerPacket _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
    }


    #region Packets
    //the different packets in which you can send the server data to the clients
    public static void Welcome(int _toClient, string _msg)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void SpawnPlayer(int _toClient, Player _player, Vector3 _spawnPoint)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.spawnPlayer))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.username);
            _packet.Write(_player.selectedCharacter);
            _packet.Write(_spawnPoint);
            SendTCPData(_toClient, _packet);
            Debug.Log($"spawning {_player.selectedCharacter} to id {_toClient}");
            ServerStart.instance.DebugServer($"spawning {_player.selectedCharacter} to id {_toClient}");
        }
    }

    public static void PlayerPosition(Player _player)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.playerPosition))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.avatar.position);
            SendUDPDataToAll(Client.instance.myId,_packet);
        }
    }

    public static void PlayerAnimation(Player _player)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.playerAnimation))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.status.animationValues.Length);
            foreach (bool _animationValue in _player.status.animationValues)
            {
                _packet.Write(_animationValue);
            }

            SendUDPDataToAll(_player.id, _packet);


        }
    }

    public static void PlayerRotation(Player _player)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.playerRotation))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.avatar.rotation);
            SendUDPDataToAll(_player.id, _packet);
        }
    }

    public static void Projectile(Player _player, int _moveIndex, Projectile projectile)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.projectile))
        {
            Server.projectiles.Add((int)GameManager.projectileNumber, projectile);
            _packet.Write(GameManager.projectileNumber);
            GameManager.projectileNumber++;

            _packet.Write(_player.projectileSpawner.position);
            _packet.Write(projectile.rotation);
            _packet.Write(_moveIndex);

            SendTCPDataToAll(_packet);
        }
    }
    
    public static void ProjectileMove(Projectile _projectile)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.projectileMove))
        {
            _packet.Write(_projectile.id);
            _packet.Write(_projectile.position);
            _packet.Write(_projectile.rotation);

            SendUDPDataToAll(_packet);
        }
    }

    public static void DestroyProjectile(Projectile _projectile)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.projectileDestroy))
        {
            _packet.Write(_projectile.id);
            SendTCPDataToAll(_packet);
        }
    }

    public static void Damage(int _playerID, float _damage, int _type)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.Damage))
        {
            _packet.Write(_damage);
            _packet.Write(_type);
            SendTCPData(_playerID, _packet);
        }
    }

    public static void LoadMenu(int menuNumber)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.LoadMenu))
        {
            if(menuNumber == 2)
            {
                foreach(ServerClient client in Server.clients.Values)
                {
                    if (client.connected)
                    {
                        client.SetCharacter();
                    }
                }
                foreach(ServerClient client in Server.clients.Values)
                {
                    if (client.connected)
                    {
                        client.SendIntoGame();
                    }
                }
                ServerStart.started = true;
            }
            Server.joinable = false;
            _packet.Write(menuNumber);
            SendTCPDataToAll(_packet);
        }
    }

    public static void SendUsernameList()
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.UsernameList))
        {
            string list = "";
            int playercount = 0;
            foreach(ServerClient client in Server.clients.Values)
            {
                if (client.connected)
                {
                    playercount++;
                    list += client.username;
                    if (client.ready)
                    {
                        list += " (ready)\n";
                    }
                    else
                    {
                        list += " (not ready)\n";
                    }
                }
            }
            _packet.Write(playercount);
            _packet.Write(list);
            SendTCPDataToAll(_packet);
        }
    }

    public static void SendMousePosition(int id, Vector2 position)
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.mousePosition))
        {
            _packet.Write(id);
            _packet.Write(position);
            SendUDPDataToAll(id, _packet);
        }
    }
    public static void SetWalls()
    {
        using (ServerPacket _packet = new ServerPacket((int)ServerPackets.SetWalls))
        {
            for (int i = 0; i < 4; i++)
            {
                _packet.Write(Walls.walls[i]);
            }
            SendUDPDataToAll(Client.instance.myId, _packet);
        }
    }
    #endregion
}
