﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public int selectedCharacter;
    public int yRotation;
    public float lastPacketTime = 0f;
    public Transform head;
    public Animator playerAnimator;
    public HUD playerHUD;
    [SerializeField] public GameObject invisible;

    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        head = GetComponent<PlayerObjectsAllocater>().Head;
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
}