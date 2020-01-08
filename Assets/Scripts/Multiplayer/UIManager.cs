using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public int selectedCharacter = 1;
    public GameObject startMenu;
    public GameObject server;
    public InputField usernameField;
    public InputField ipAdress;
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
        selectedCharacter = n; 
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Client.instance.ConnectToServer();
        Destroy(GameObject.Find("Main Camera"));
    }

    public void changeIP()
    {
        Client.instance.ip = ipAdress.text;
    }

    public void CreateServer()
    {
        Instantiate(server);

    }
}