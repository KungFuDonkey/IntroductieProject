﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class ServerStart : MonoBehaviour
{
    // Start the server
    public static ServerStart instance;
    Text content;
    GameObject serverLog;
    public static bool started = false;
    public static List<int> destroyId = new List<int>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Client.instance.host = true;
        serverLog = (GameObject)Instantiate(Resources.Load("ServerLog"));
        serverLog.SetActive(false);
        content = serverLog.transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Text>();
        Server.Start(25, 26950);
    }


    //let the server run on fixed ticks
    void FixedUpdate()
    {
        bool reset = false;
        if (started)
        {
            foreach (ServerClient _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    _client.player.UpdatePlayer();
                    if (!_client.player.status.alive)
                    {
                        _client.player = null;
                    }
                    if (_client.player.evolve)
                    {
                        int selectedCharacter = _client.player.selectedCharacter + 1 % 3;
                        _client.player = Server.characters[selectedCharacter](_client.id, Server.clients[_client.id].username, selectedCharacter);
                        ServerSend.Evolve(_client.id, selectedCharacter);
                    }
                }
            }
            foreach (Projectile _projectile in Server.projectiles.Values)
            {
                _projectile.UpdateProjectile();
            }
            foreach (int i in destroyId)
            {
                Server.projectiles.Remove(i);
                reset = true;
            }
            Walls.UpdateWalls();
        }
        else if (!Server.joinable)
        {
            foreach(ServerClient _client in Server.clients.Values)
            {
                if (_client.connected)
                {
                    ServerSend.SendMousePosition(_client.id, _client.mousePosition);
                }
            }
        }
        if (reset)
        {
            destroyId = new List<int>();
        }
        if (Input.GetKey(KeyCode.I))
        {
            serverLog.SetActive(true);
        }
        if (Input.GetKey(KeyCode.O))
        {
            serverLog.SetActive(false);
        }
    }
    public void DebugServer(string message)
    {
        content.text += message + "\n";
    }
    public static void SpawnItem()
    {
        Debug.Log("spawning Items");
        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-300, 300), 1, Random.Range(-300, 300));
            int item = Random.Range(0, 7);
            GameObject prop = Instantiate(GameManager.instance.Items[item], pos, Quaternion.identity);
            gameItem thisItem = prop.GetComponentInChildren<gameItem>();
            thisItem.id = i;
            GameManager.instance.gameItems[i] = prop;
            Thread.Sleep(10);
            RaycastHit hit;
            if (Physics.Raycast(GameManager.instance.gameItems[i].transform.position, -Vector3.up, out hit))
            {
                pos.y = hit.point.y + 1;
                GameManager.instance.gameItems[i].transform.position = pos;
            }
            else if (Physics.Raycast(GameManager.instance.gameItems[i].transform.position, Vector3.up, out hit))
            {
                pos.y = hit.point.y - 1;
            }
            ServerSend.SpawnItem(i, item, GameManager.instance.gameItems[i].transform.position);
            Thread.Sleep(10);
        }
    }
}
