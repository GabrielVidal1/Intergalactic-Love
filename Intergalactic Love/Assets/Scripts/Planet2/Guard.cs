using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float thresoldDistant;


    private NavMeshAgent agent;
    private int index;

    private bool isChasingPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        index = 0;
        isChasingPlayer = false;

        agent.destination = points[index].position;
    }

    void Update()
    {
        if (!isChasingPlayer)
        {
            if (Vector3.Distance(transform.position, points[index].position) < thresoldDistant )
            {
                index = (index + 1) % points.Length;
                agent.destination = points[index].position;
            }
        }
        else
        {
            agent.destination = GameManager.gm.player.transform.position;
        }
    }



}
