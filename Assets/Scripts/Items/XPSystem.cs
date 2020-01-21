using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPSystem : MonoBehaviour
{
    [SerializeField] private GameObject TekstLevelUp;
    [SerializeField] private GameObject FireMoveE;
    [SerializeField] private GameObject FireMoveQ;
    [SerializeField] private GameObject WaterMoveE;
    [SerializeField] private GameObject WaterMoveQ;
    public Text LevelCountText;
    public Text LevelText;
    public Text NextText;
    PlayerManager character;
    int XP;
    public int CurrentLevel;


    public static XPSystem instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        TekstLevelUp.SetActive(false);
        FireMoveE.SetActive(false);
        FireMoveQ.SetActive(false);
        WaterMoveE.SetActive(false);
        WaterMoveQ.SetActive(false);
        character = GameObject.Find(Client.instance.myId.ToString()).GetComponent<PlayerManager>();
    }
    public void NewLevel()
    {
        TekstLevelUp.SetActive(false);
    }
    public void Emove()
    {
        if(character.selectedCharacter == 0)
        {
            WaterMoveE.SetActive(true);
        }
        else if(character.selectedCharacter == 1)
        {
            //EarthmoveE.SetActive(true);
        }
        else
        {
            FireMoveE.SetActive(true);
        }
    }
    public void Qmove()
    {
        if (character.selectedCharacter == 0)
        {
            WaterMoveQ.SetActive(true);
        }
        else if (character.selectedCharacter == 1)
        {
            //EarthmoveE.SetActive(true);
        }
        else
        {
            FireMoveQ.SetActive(true);
        }
    }
    public void WaitNewLevel()
    {
        TekstLevelUp.SetActive(true);
        Invoke("NewLevel", 3);
    }

    void Update()
    {
        XPUpdate(2);
        MovesUpdate();
    }

    public void XPUpdate(int xp)
    {
        XP += xp;
        int level = (int)(0.1f * Mathf.Sqrt(XP));
        string levelCount = level.ToString();
        
        if (level != CurrentLevel && level != 1)
        {
            CurrentLevel = level;
            WaitNewLevel();
        }
        int XPForNextLevel = 100 * (CurrentLevel + 1) * (CurrentLevel + 1);
        

        int XPDifference = XPForNextLevel - XP;
        string XPNextLevel = XPDifference.ToString();
        if (level == 0 || level == 1)
        {
            levelCount = 1.ToString();
            XPNextLevel = 0.ToString();
        }

        LevelCountText.text = levelCount;
        int Difference = XPForNextLevel - (100 * CurrentLevel * CurrentLevel);

       
        LevelText.text = levelCount;
        NextText.text = XPNextLevel;
    }

    public void MovesUpdate()
    {
        if (CurrentLevel >= 3) 
        {
            Emove();
        }
        if (CurrentLevel >= 5 )
        {
            Qmove();
        }
    }
}
