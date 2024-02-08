using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 targetPos;
    bool isGoing = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isGoing)
        {
            isGoing = true;

            // ƒ‰ƒ“ƒ_ƒ€‚És‚«æ‚ğŒˆ’è
            List<Vector3> targetPosList = new List<Vector3>();
            foreach (GameObject point in GameObject.FindGameObjectsWithTag("Point"))
            {
                targetPosList.Add(point.transform.position);
            }
            targetPosList = targetPosList.OrderBy(a => Guid.NewGuid()).ToList();
            targetPos = targetPosList[0];

            agent.destination = targetPos;
        }

        if (agent.remainingDistance < 0.5f)
        {
            isGoing = false;
        }
    }
}
