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
    public float timer = 10f, loadTime, LOADTIME = 5;
    public GameObject startMenu, lobby, characterSelection, loadingScreen;
    public GameObject server;
    public GameObject startButton;
    public GameObject myCursor;
    public InputField usernameField;
    public InputField ipAdress;
    public Text serverInfo, playerCount;
    public Text UsernameList;

    public List<OnlineMousePointer> mousePointers = new List<OnlineMousePointer>();

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

    private void Start()
    {
        for(int i = 0; i<25; i++)
        {
            mousePointers.Add(null);
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
        if(menu == 0)
        {
            characterSelection.SetActive(false);
            lobby.SetActive(true);
        }
        else if(menu == 1)
        {
            lobby.SetActive(false);
            characterSelection.SetActive(true);
            for(int i = 1; i <= int.Parse(playerCount.text); i++)
            {
                if(i != Client.instance.myId)
                {
                    GameObject onlineCursor = (GameObject)Instantiate(Resources.Load("OnlineCursor"),characterSelection.transform);
                    OnlineMousePointer mp = onlineCursor.GetComponent<OnlineMousePointer>();
                    mousePointers[i] = mp;
                }
                else
                {
                    myCursor = (GameObject)Instantiate(Resources.Load("Cursor"), characterSelection.transform);
                }
            }
            startCounter = true;
        }
        else if(menu == 2)
        {
            characterSelection.SetActive(false);
            loadingScreen.SetActive(true);
            LoadingScreen.loading();
            loadTime += Time.deltaTime;
            if (loadTime >= LOADTIME)
            {
                setMenuStatus(false);
                Destroy(GameObject.Find("Main Camera"));
                GameManager.instance.freezeInput = false;
            }
        }
    }

    public void SetUsernameList(string list)
    {
        UsernameList.text = list;
    }

    public void SetPlayerCount(int playercount)
    {
        playerCount.text = playercount.ToString();
    }

    public void ChangeReady()
    {
        ClientSend.Ready();
    }

    public void StartButton(bool ready)
    {
        if (ready)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
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

    public void setMenuStatus(bool setStatus)
    {
        gameObject.SetActive(setStatus);
    }

    public void ResetUI()
    {
        for(int i = mousePointers.Count - 1; i >= 0; i--)
        {
            Destroy(mousePointers[i].gameObject);
            mousePointers.RemoveAt(i);
        }
        startCounter = false;
        timer = 10f;
    }
}