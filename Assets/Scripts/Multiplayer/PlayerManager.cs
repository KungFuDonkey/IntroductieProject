using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public int selectedCharacter;
    public static float yRotation;
    public float lastPacketTime = 0f;
    public Transform head;
    public Animator playerAnimator;
    public HUD playerHUD;
    public Dictionary<int, Quaternion> quaternionCalc = new Dictionary<int, Quaternion>()
    {
        { (int)Characters.Vulcasaur, Quaternion.Euler(-1.14f, 17.087f, yRotation) },
        { (int)Characters.McQuirtle, Quaternion.Euler(yRotation, 17.974f, 0f) },
        { (int)Characters.Charmandolphin, Quaternion.Euler(yRotation, 0f, 0f) }
    };
    [SerializeField] public GameObject invisible;

    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }

    private void LateUpdate()
    {
        if(id != Client.instance.myId)
        {
            head.localRotation = quaternionCalc[selectedCharacter];
        }
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
        if(screen == 0)
        {
            playerHUD.Deathscreen.SetActive(true);
        }
        else
        {
            playerHUD.Winscreen.SetActive(true);
        }
    }

    public void setYRotation(float rotation)
    {
        yRotation = rotation;
    }
}