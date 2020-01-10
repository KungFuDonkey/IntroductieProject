using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerStart : MonoBehaviour
{
    // Start the server
    public static ServerStart instance;
    Text content;
    GameObject serverLog;

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
        serverLog = (GameObject)Instantiate(Resources.Load("ServerLog"));
        serverLog.SetActive(false);
        content = serverLog.transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Text>();
        Server.Start(50, 26950);
    }


    //let the server run on fixed ticks
    void FixedUpdate()
    {
        foreach (ServerClient _client in Server.clients.Values)
        {
            if (_client.player != null)
            {
                _client.player.UpdatePlayer();
            }
        }
        foreach(Projectile _projectile in Server.projectiles.Values)
        {
            _projectile.UpdateProjectile();
        }
        if (Input.GetKey(KeyCode.P))
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
}
