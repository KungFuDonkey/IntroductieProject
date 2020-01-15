using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public int selectedCharacter;
    public int yRotation;
    public float lastPacketTime = 0f;
    public Animator playerAnimator;
    public Transform player;
    public Player playert;
    [SerializeField] public GameObject invisible;

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
}