using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetID : MonoBehaviour
{
    public Material corNPC;
    public Material corTarget;
    public Material corInfo;
    public Material corInno;
    

    private void OnTriggerEnter(Collider colider)
    {
        if(colider.gameObject.tag == "Target")
        {
            colider.GetComponentInChildren<Renderer>().sharedMaterial = corTarget;
        }
        if (colider.gameObject.tag == "Info")
        {
            colider.GetComponentInChildren<Renderer>().sharedMaterial = corInfo;
        }
        if (colider.gameObject.tag == "NPC")
        {
            colider.GetComponent<NavegadorPontos>().tocando = true;
        }

    }

    private void OnTriggerExit(Collider colider)
    {
        if (colider.gameObject.tag == "Target")
        {
            colider.GetComponentInChildren<Renderer>().sharedMaterial = corNPC;
        }
        if (colider.gameObject.tag == "Info")
        {
            colider.GetComponentInChildren<Renderer>().sharedMaterial = corNPC;
        }
        if (colider.gameObject.tag == "NPC")
        {
            colider.GetComponent<NavegadorPontos>().tocando = false;
        }
    }
}
