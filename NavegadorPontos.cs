using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavegadorPontos : MonoBehaviour
{
    public NavMeshAgent agent;
    public Waypoint pontoAtual;
    public int direcao;

    public bool chegou;
    public bool tocando = false;

    void Start()
    {
        agent.SetDestination(pontoAtual.GetPosition());
    }

    
    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            chegou = true;
        }
        #region se chegou ao ponto
        if (chegou == true)
        {
            bool shouldBranch = false;

            if(pontoAtual.branches != null && pontoAtual.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= pontoAtual.branchRatio ? true : false;
            }

            if(shouldBranch == true)
            {
                pontoAtual = pontoAtual.branches[Random.Range(0, pontoAtual.branches.Count - 1)];
                agent.SetDestination(pontoAtual.GetPosition());
            }
            else
            {
                if (direcao == 1)
                {
                    if(pontoAtual.pontoSeguinte != null)
                    {
                        pontoAtual = pontoAtual.pontoSeguinte;
                        agent.SetDestination(pontoAtual.GetPosition());
                        chegou = false;
                    }
                    else
                    {
                        pontoAtual = pontoAtual.pontoAnterior;
                        agent.SetDestination(pontoAtual.GetPosition());
                        direcao = 0;
                        chegou = false;
                    }
                    
                }
                else if (direcao == 0)
                {
                    if(pontoAtual.pontoAnterior != null)
                    {
                        pontoAtual = pontoAtual.pontoAnterior;
                        agent.SetDestination(pontoAtual.GetPosition());
                        chegou = false;
                    }
                    else
                    {
                        pontoAtual = pontoAtual.pontoSeguinte;
                        agent.SetDestination(pontoAtual.GetPosition());
                        direcao = 1;
                        chegou = false;
                    }
                }
            }//fim do else 

        }//fim do if de chegada
        #endregion
    }//fim do update

}
