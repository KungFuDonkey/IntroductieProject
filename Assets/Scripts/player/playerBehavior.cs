using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float lives = 100;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void hit(float damage, string type)
    {
        lives -= damage;
        if (lives <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetTrigger("Die");
    }
}
