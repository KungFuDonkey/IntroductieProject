using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public int selectedCharacter = 1;
    public InputField usernameField;
    public List<OnlineMousePointer> mousePointers = new List<OnlineMousePointer>();
    [SerializeField] InputField ipAdress;
    [SerializeField] Text serverInfo, playerCount, UsernameList;
    [SerializeField] Image loadingBar;
    [SerializeField] GameObject startMenu, lobby, characterSelection, loadingScreen;
    [SerializeField] GameObject server;
    [SerializeField] GameObject startButton;
    public GameObject startScreen;
    float timer = 10f, loadTime, LOADTIME = 1;
    bool startCounter = false, loading = false;


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
        for(int i = 0; i<10; i++)
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
                    GameObject myCursor = (GameObject)Instantiate(Resources.Load("Cursor"), characterSelection.transform);
                }
            }
            startCounter = true;
        }
        else if(menu == 2)
        {
            loading = true;
            loadingScreen.SetActive(true);
            characterSelection.SetActive(false);
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
        if (loading)
        {
            float progress = Mathf.Clamp(loadTime / LOADTIME * 290, 0, 290);
            loadingBar.rectTransform.sizeDelta = new Vector2(progress, 20);
            loadingBar.rectTransform.localPosition = new Vector2(progress / 2 - 145, 0);
            loadTime += Time.deltaTime;
            if (loadTime >= LOADTIME + 0.1f)
            {
                ServerStart.started = true;
                GameManager.instance.BattleBus.GetComponent<AudioSource>().Play();
                setMenuStatus(false);
                Destroy(startScreen);
                GameManager.instance.freezeInput = false;
                loadTime = 0;
            }
        }
    }

    public void setMenuStatus(bool setStatus)
    {
        gameObject.SetActive(setStatus);
    }

    public void ResetUI()
    {
        loading = false;
        for(int i = mousePointers.Count - 1; i >= 0; i--)
        {
            try
            {
                Destroy(mousePointers[i].gameObject);
            }
            catch
            {

            }
            mousePointers[i] = null;
        }
        loadingScreen.SetActive(false);
        startCounter = false;
        timer = 10f;
    }
}