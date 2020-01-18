using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPSystem : MonoBehaviour
{
    [SerializeField] private GameObject TekstLevelUp;
    public Text LevelCountText;
    int XP;
    int CurrentLevel;

    public void NewLevel()
    {
        TekstLevelUp.SetActive(false);
    }

    public void WaitNewLevel()
    {
        TekstLevelUp.SetActive(true);
        Invoke("NewLevel", 2);
    }

    void Update()
    {
        XPUpdate(2);
    }

    public void XPUpdate(int xp)
    {
        XP += xp;
        //Debug.Log(XP);
        int level = (int)(0.1f * Mathf.Sqrt(XP));
        string levelCount = level.ToString();
        LevelCountText.text = levelCount;
        //Debug.Log(level);
        
        if (level != CurrentLevel)
        {
            CurrentLevel = level;
            WaitNewLevel();
        }

        int XPForNextLevel = 100 * (CurrentLevel + 1) ^ 2;
        int XPDifference = XPForNextLevel - XP;

        int Difference = XPForNextLevel - (100 * CurrentLevel ^ 2);


    }
}
