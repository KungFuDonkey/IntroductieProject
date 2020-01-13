using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public int selectedCharacter = 1;
    public bool startCounter = false;
    public float timer = 10f;
    public GameObject mainCamera;
    public GameObject startMenu, lobby, characterSelection;
    public GameObject server;
    public InputField usernameField;
    public InputField ipAdress;
    public Text serverInfo;
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
    }

    public void SelectMyCharacter(int n)
    {
        ClientSend.ChoosePlayer(n);
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        lobby.SetActive(true);
        usernameField.interactable = false;
        if(ipAdress.text != "")
        {
            Client.instance.ip = ipAdress.text;
        }
        Client.instance.ConnectToServer();
    }

    public void CreateServer()
    {
        GameObject currentserver = GameObject.Find("Server(Clone)");
        if(currentserver != null)
        {
            Destroy(currentserver);
        }
        Instantiate(server);
        Client.instance.ConnectToServer();
        startMenu.SetActive(false);
        lobby.SetActive(true);
    }

    public void startCharacterSelection()
    {
        ServerSend.LoadMenu(1);
    }

    public void LoadMenu(int menu)
    {
        if(menu == 1)
        {
            lobby.SetActive(false);
            characterSelection.SetActive(true);
            startCounter = true;
        }
        else if(menu == 2){
            gameObject.SetActive(false);
            Destroy(mainCamera);
        }
    }

    private void Update()
    {
        if (startCounter)
        {
            timer -= Time.deltaTime;
            if(timer < 0 && Client.instance.host)
            {
                ServerSend.LoadMenu(2);
                timer = 100f;
            }
        }
    }
}