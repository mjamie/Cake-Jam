using ECM.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    private bool lostHealth = false;

    private float imunnityTime = 0;
    private Animator animator;

    [SerializeField] private float imunnityTimeMax = 2;

    public int Health { get => health; set => health = value; }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (lostHealth)
        {
            imunnityTime += Time.deltaTime;

            if (imunnityTime >= imunnityTimeMax)
            {
                imunnityTime = 0;
                lostHealth = false;
            }
        }
    }

    public void LoseHealth()
    {
        if (!lostHealth)
        {

            print("Lost health");
            health--;

            lostHealth = true;
            if (health <= 0)
            {
                //Death

                animator.SetTrigger("isDead");
                GetComponent<BaseCharacterController>().pause = true;
                print("deadf");
                return;
            }

            animator.SetTrigger("isHit");
        }
    }

    public void GainHealth()
    {
        if (health >= 3)
            return;

        health++;
    }

}
