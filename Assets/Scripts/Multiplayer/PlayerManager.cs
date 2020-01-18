using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject Allparts;
    public int id;
    public string username;
    public int selectedCharacter;
    public float lastPacketTime = 0f;
    public Animator playerAnimator;
    public HUD playerHUD;
   public bool invisible;

    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }

    public void SetAnimations(bool[] animationValues)
    {
        playerAnimator.SetBool("IsWalking", animationValues[0]);
        playerAnimator.SetBool("IsRunning", animationValues[1]);
        if (animationValues[2])
        {
            playerAnimator.SetTrigger("Attack");
        }

    }

    public void Invisible()
    {
        if (invisible == false)
        {
            Allparts.SetActive(false);
        }
    }

 
    public void UpdateHUD(float health, float shield)
    {
        playerHUD.healthBar.currentHealth = health;
        playerHUD.shieldBar.currentShield = shield;
    }

    public void UpdatePlayerCount(int alive)
    {
        playerHUD.AlivePlayers.text = alive.ToString();
    }

    public void Screen(int screen)
    {
        GameManager.instance.freezeInput = true;
        if(screen == 0)
        {
            playerHUD.Deathscreen.SetActive(true);
        }
        else
        {
            playerHUD.Winscreen.SetActive(true);
        }
    }
}