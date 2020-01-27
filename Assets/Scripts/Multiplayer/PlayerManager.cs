using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject Allparts;
    public PlayerController controller;
    public GameObject evolution1;
    public GameObject evolution2;
    public GameObject evolution3;
    public int id;
    public string username;
    public int selectedCharacter;
    public Transform head;
    public static float yRotation;
    public float lastPacketTime = 0f;
    public Animator playerAnimator;
    public HUD playerHUD;
    private int evolutionStage = 1;

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

    public void Evolve(int evolutionStage)
    {
        GameObject oldEvolution = null;
        GameObject newEvolution = null;
        PlayerObjectsAllocater allocater;
        if (evolutionStage == 2)
        {
            oldEvolution = evolution1;
            newEvolution = evolution2; 
        }
        else if (evolutionStage == 3)
        {
            oldEvolution = evolution2;
            newEvolution = evolution3;
        }
        oldEvolution.SetActive(false);
        newEvolution.SetActive(true);
        allocater = newEvolution.GetComponent<PlayerObjectsAllocater>();
        head = allocater.Head;
        playerAnimator = allocater.playerAnimator;
        Allparts = allocater.AllParts;
        controller.playerAnimator = playerAnimator;

    }
    private void LateUpdate()
    {
        if (id != Client.instance.myId)
        {
            if (selectedCharacter == 0)
            {
                head.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
            }
            else if (selectedCharacter == 1)
            {
                head.localRotation = Quaternion.Euler(yRotation, 17.974f, 0f);
            }
            else if (selectedCharacter == 2)
            {
                head.localRotation = Quaternion.Euler(-1.14f, 17.087f, yRotation);
            }
        }
    }

    public void Invisible(bool invisible)
    {
        Allparts.SetActive(!invisible);
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
        /*
        GameManager.instance.freezeInput = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (screen == 0)
        {
            Die();
            playerHUD.Deathscreen.SetActive(true);
        }
        else
        {
            playerHUD.Winscreen.SetActive(true);
        }
        */
    }

    public void Die()
    {
        playerAnimator.SetTrigger("Die");
    }

    public void setYRotation(float rotation)
    {
        yRotation = rotation;
    }
   
}