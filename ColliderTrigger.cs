using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public GameObject targetToActiv;
    public Material corInno;
    public GameObject feedTarget;

    public int numInnos = 0;

    private void OnTriggerEnter(Collider colider)
    {
        if (colider.gameObject.tag == "Info")
        {
            targetToActiv.SetActive(true);
            feedTarget.SetActive(true);
        }

        if (colider.gameObject.tag == "NPC" && colider.GetComponentInChildren<Renderer>().sharedMaterial == corInno)
        {
            numInnos++;
        }

    }

    private void OnTriggerExit(Collider colider)
    {
        if (colider.gameObject.tag == "Info")
        {
            targetToActiv.SetActive(true);
            feedTarget.SetActive(true);
        }

        if (colider.gameObject.tag == "NPC" && colider.GetComponentInChildren<Renderer>().sharedMaterial == corInno)
        {
                numInnos++;
        }

    }

    private void Update()
    {
        if (numInnos >= 2)
        {
            targetToActiv.SetActive(true);
            feedTarget.SetActive(true);
        }
    }
}
