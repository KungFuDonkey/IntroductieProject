using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameServer
{
    public class AssignObjectToServer : MonoBehaviour
    {
        Client client;
        public PlayerManager playerManager;
        private void Start()
        {
            client = GameObject.Find("ClientManager").GetComponent<Client>();
            if (!client.host)
            {
                Destroy(this);
            }
        }
        private void Update()
        {
            if(playerManager.id != 0)
            {
                foreach (ServerClient c in Server.clients.Values)
                {
                    if(c.id == playerManager.id)
                    {
                        c.player.controller = gameObject.GetComponent<CharacterController>();
                        c.player.groundCheck = transform.GetChild(transform.childCount - 1);
                        c.player.avatar = transform;
                        Debug.Log("Player Assigned");
                        Destroy(this);
                    }
                }
            }
        }
    }
}
