using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if(ipAdress.text != "")
        {
            Client.instance.ip = ipAdress.text;
        }
        Client.instance.ConnectToServer();
        Destroy(GameObject.Find("Main Camera"));
    }

    public void CreateServer()
    {
        GameObject currentserver = GameObject.Find("Server(Clone)");
        if(currentserver != null)
        {
            Destroy(currentserver);
        }
        Instantiate(server);
    }
    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}