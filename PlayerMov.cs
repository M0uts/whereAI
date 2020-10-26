using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerMov : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    public Material corTarget;
    public Material corInno;

    public Menus menu;
    public Animator animator;

    public int marcados = 0;

    private void Start()
    {
        if(animator != null)
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(Menus.emPausa == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit;
                //if (Physics.Raycast(ray, out hit))
                var multipleHits = Physics.RaycastAll(ray);
                foreach (var raycastHit in multipleHits)
                {
                    if (raycastHit.transform.CompareTag("Target")
                        && raycastHit.transform.GetComponentInChildren<Renderer>().sharedMaterial == corTarget)
                    {
                        menu.VictoryMenu();
                    }

                    if (SceneManager.GetActiveScene().buildIndex != 1)
                    {
                        if (raycastHit.transform.CompareTag("NPC") && raycastHit.transform.GetComponent<NavegadorPontos>().tocando == true)
                        {
                            if (marcados == 3) menu.LoseMenu();
                            else
                            {
                                marcados++;
                                raycastHit.transform.GetComponentInChildren<Renderer>().sharedMaterial = corInno;
                            }
                        }
                    }

                    if (raycastHit.transform.CompareTag("Info"))
                    {
                        raycastHit.transform.GetComponent<NaveInfo>().perguntado = true;
                    }

                    if(raycastHit.transform.CompareTag("Floor"))
                    {
                        animator.SetBool("andar", true);
                        agent.SetDestination(raycastHit.point);
                    }
                }
            }
        }

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            animator.SetBool("andar", false);
            agent.CompleteOffMeshLink();
        }
    }//fim do update
}
