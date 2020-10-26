using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaveInfo : MonoBehaviour
{
    public GameObject target;
    public GameObject infoPos;
    public GameObject feedtarget;
    public GameObject feedLoca;

    public NavMeshAgent agent;

    public Animator animator;

    public bool perguntado = false;

    // Update is called once per frame
    void Update()
    {
        if (perguntado)
        {
            if(target != null)
            {
                if (feedtarget != null)
                    feedtarget.SetActive(true);
                target.SetActive(true);
                agent.SetDestination(target.transform.position);
                animator.SetBool("andar", true);
            }
            if(infoPos != null)
            {
                if (feedLoca != null)
                    feedLoca.SetActive(true);
                infoPos.SetActive(true);
                agent.SetDestination(infoPos.transform.position);
                animator.SetBool("andar", true);
            }
        }

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            animator.SetBool("andar", false);
            agent.CompleteOffMeshLink();
        }
    }//fim do update
}
