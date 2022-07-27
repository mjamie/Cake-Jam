using ECM.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    GameObject player;

    private bool inBush = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("enter");
            inBush = true;
        }
    }

    private void Update()
    {
        if (inBush)
        {

            print(player.GetComponentInChildren<Animator>().GetBool("isCrouching") + "\n" +
               player.GetComponent<BaseCharacterController>().isHidden);

            if (player.GetComponentInChildren<Animator>().GetBool("isCrouching"))
            {
                player.GetComponent<BaseCharacterController>().isHidden = true;
            }
            else
            {
                player.GetComponent<BaseCharacterController>().isHidden = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inBush = false;
            player.GetComponent<BaseCharacterController>().isHidden = false;
        }
    }
}
