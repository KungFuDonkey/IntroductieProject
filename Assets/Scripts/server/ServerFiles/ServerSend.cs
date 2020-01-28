using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ServerSend 
{
    //functions for sending TCP or UDP data to different clients
    private static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    private static void SendUDPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }
    private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
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

    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }
    private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
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
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
        }
    }

    public static void SpawnPlayer(int _toClient, Player _player, Vector3 _spawnPoint)
    {
        using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
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
        using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
        {
            _packet.Write(Time.time);
            _packet.Write(_player.id);
            _packet.Write(_player.avatar.position);
            SendUDPDataToAll(Client.instance.myId,_packet);
        }
    }

    public static void PlayerAnimation(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerAnimation))
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
        using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.avatar.rotation);
            _packet.Write(_player.verticalRotation);
            SendUDPDataToAll(_player.id, _packet);
        }
    }

    public static void Evolve(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.Evolve))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.evolutionStage);
            SendTCPDataToAll(_packet);
        }
    }

    public static void Projectile(Player _player, int _moveIndex, Projectile projectile)
    {
        using (Packet _packet = new Packet((int)ServerPackets.projectile))
        {
            Server.projectiles.Add(GameManager.projectileNumber, projectile);
            _packet.Write(GameManager.projectileNumber);
            GameManager.projectileNumber++;

            _packet.Write(_player.projectileSpawner.position);
            _packet.Write(_player.id);
            _packet.Write(projectile.rotation);
            _packet.Write(_moveIndex);

            SendTCPDataToAll(_packet);
        }
    }
    
    public static void ProjectileMove(Projectile _projectile)
    {
        using (Packet _packet = new Packet((int)ServerPackets.projectileMove))
        {
            _packet.Write(Time.time);
            _packet.Write(_projectile.id);
            _packet.Write(_projectile.position);
            _packet.Write(_projectile.rotation);

            SendUDPDataToAll(_packet);
        }
    }

    public static void DestroyProjectile(Projectile _projectile)
    {
        using (Packet _packet = new Packet((int)ServerPackets.projectileDestroy))
        {
            _packet.Write(_projectile.id);
            SendTCPDataToAll(_packet);
        }
    }

    public static void LoadMenu(int menuNumber)
    {
        using (Packet _packet = new Packet((int)ServerPackets.LoadMenu))
        {
            if (menuNumber == 2)
            {
                UIManager.instance.LoadMenu(2);
                foreach (ServerClient client in Server.clients.Values)
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
                ServerStart.SpawnItem();
                ScoreboardSetUp();
            }
            Server.joinable = false;
            _packet.Write(menuNumber);
            SendTCPDataToAll(_packet);
            UpdatePlayerCount();
        }
    }

    public static void SendUsernameList()
    {
        using (Packet _packet = new Packet((int)ServerPackets.UsernameList))
        {
            string list = "";
            int playercount = 0;
            bool start = true;
            foreach(ServerClient client in Server.clients.Values)
            {
                if (client.connected)
                {
                    playercount++;
                    list += client.username;
                    if(client.id == Client.instance.myId)
                    {
                        list += " (master client)\n";
                    }
                    else if (client.ready)
                    {
                        list += " (ready)\n";
                    }
                    else
                    {
                        list += " (not ready)\n";
                        start = false;
                    }
                }
            }
            _packet.Write(playercount);
            _packet.Write(list);
            _packet.Write(start);
            SendTCPDataToAll(_packet);
        }
    }

    public static void SendMousePosition(int id, Vector2 position)
    {
        using (Packet _packet = new Packet((int)ServerPackets.mousePosition))
        {
            _packet.Write(id);
            _packet.Write(position);
            SendUDPDataToAll(id, _packet);
        }
    }

    public static void SetWalls()
    {
        using (Packet _packet = new Packet((int)ServerPackets.SetWalls))
        {
            for (int i = 0; i < 4; i++)
            {
                _packet.Write(Walls.walls[i].position);
                _packet.Write(Walls.walls[i].localScale);
            }
            SendUDPDataToAll(Client.instance.myId, _packet);
        }
    }

    public static void UpdateHUD(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.UpdateHUD))
        {
            _packet.Write(_player.status.health);
            _packet.Write(_player.status.shield);
            SendUDPData(_player.id,_packet);
        }
    }

    public static void SetInvis(int id, bool invis)
    {
        using (Packet _packet = new Packet((int)ServerPackets.SetInvis))
        {
            Debug.Log("sending invis to clients");
            _packet.Write(id);
            _packet.Write(invis);
            SendTCPDataToAll(_packet);
        }
    }

    public static void UpdatePlayerCount()
    {
        using (Packet _packet = new Packet((int)ServerPackets.UpdatePlayerCount))
        {
            int alive = 0;
            foreach(ServerClient client in Server.clients.Values)
            {
                if(client.player != null)
                {
                    if (client.player.status.alive)
                    {
                        alive++;
                    }
                }
            }
            _packet.Write(alive);
            SendTCPDataToAll(_packet);
            ScoreboardUpdate();
            if (alive <= 1)
            {
                SendWinScreen();
            }
        }
    }

    public static void SendWinScreen()
    {
        using (Packet _packet = new Packet((int)ServerPackets.Win))
        {
            ServerStart.instance.resetScreen.SetActive(true);
            GameManager.instance.freezeInput = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            foreach (ServerClient client in Server.clients.Values)
            {
                if (client.player.status.alive)
                {
                    SendTCPData(client.id, _packet);
                    return;
                }
            }
        }
    }

    public static void SendDeathScreen(Player player)
    {
        using(Packet _packet = new Packet((int)ServerPackets.Death))
        {
            _packet.Write(player.id);
            SendTCPDataToAll(_packet);
        }
    }

    public static void Reset()
    {
        using(Packet _packet = new Packet((int)ServerPackets.Reset))
        {
            SendTCPDataToAll(_packet);
        }
    }

    public static void SpawnItem(int key, int item, Vector3 position)
    {
        using(Packet _packet = new Packet((int)ServerPackets.SpawnItem))
        {
            _packet.Write(key);
            _packet.Write(item);
            _packet.Write(position);
            SendTCPDataToAll(Client.instance.myId, _packet);
        }
    }

    public static void Item(int id, int itemNumber, int toClient)
    {
        using(Packet _packet = new Packet((int)ServerPackets.Item))
        {
            _packet.Write(id);
            _packet.Write(itemNumber);
            SendTCPData(toClient, _packet);
        }
        RemoveItem(id);
    }

    public static void RemoveItem(int id)
    {
        using(Packet _packet = new Packet((int)ServerPackets.RemoveItem))
        {
            _packet.Write(id);
            SendTCPDataToAll(_packet);
        }
    }

    public static void SetBus()
    {
        using(Packet _packet = new Packet((int)ServerPackets.SetBus))
        {
            _packet.Write(BattleBus.Bus.position);
            SendTCPDataToAll(_packet);
        }
    }

    public static void StormOverlay(int id, bool storm)
    {
        using (Packet _packet = new Packet((int)ServerPackets.StormOverlay))
        {
            _packet.Write(storm);
            SendTCPData(id, _packet);
        }
    }

    public static void BusCamera(int id, bool bus)
    {
        using (Packet _packet = new Packet((int)ServerPackets.BusCamera))
        {
            _packet.Write(bus);
            SendTCPData(id, _packet);
        }
    }

    public static void ScoreboardUpdate()
    {
        using (Packet _packet = new Packet((int)ServerPackets.ScoreboardUpdate))
        {
            Debug.Log("UpdateServer");
            List<int[]> scores = new List<int[]>();
            int i = 0;
            foreach (ServerClient client in Server.clients.Values)
            {
                if (client.connected)
                {
                    i++;
                    int[] score = new int[2];
                    score[0] = client.player.kills;
                    score[1] = client.player.damage;
                    scores.Add(score);
                }
            }
            _packet.Write(i);
            for (int j = 0; j < i; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    _packet.Write(scores[j][k]);
                }
            }
            SendTCPDataToAll(_packet);
        }
    }

    public static void ScoreboardSetUp()
    {
        using (Packet _packet = new Packet((int)ServerPackets.ScoreboardSetUp))
        {
            Debug.Log("SetUpServer");
            List<string> usernames = new List<string>();
            int i = 0;
            foreach (ServerClient client in Server.clients.Values)
            {
                if (client.connected)
                {
                    i++;
                    string username = Server.clients[i].username;
                    if (username == null)
                    {
                        username = (i).ToString();
                    }
                    usernames.Add(username);
                }
            }
            _packet.Write(i);
            for (int j = 0; j < i; j++)
            {
                _packet.Write(usernames[j]);
            }
            SendTCPDataToAll(_packet);
        }
    }
    #endregion
}
