using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;
using ECM.Controllers;

public class Patrol : MonoBehaviour
{
    public Transform[] points;

    public float minDis = 5;

    private int destPoint = 0;
    private bool inVision = false;
    private bool chasing = false;

    private NavMeshAgent agent;
    private TriggerSensor triggerSensor;

    private float pauseTime = 0;
    private float pastTime = 0;

    GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        triggerSensor = GetComponent<TriggerSensor>();

        pauseTime = Random.Range(1, 3);

        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = true;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;

        pauseTime = Random.Range(3, 5);
        pastTime = 0;
    }


    void Update()
    {
        inVision = false;

        foreach (var item in triggerSensor.DetectedObjects)
        {
            if (item.CompareTag("Player"))
            {
                if (player.GetComponent<BaseCharacterController>().isHidden)
                    return;

                inVision = true;
                agent.destination = player.transform.position;
                return;
            }
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= minDis && !inVision)
        {
            if (player.GetComponent<BaseCharacterController>().isHidden)
                return;

            chasing = true;
            agent.destination = player.transform.position;
            return;
        }

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            pastTime += Time.deltaTime;

            if (pauseTime <= pastTime)
                GotoNextPoint();
        }
    }
}
