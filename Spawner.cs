using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public GameObject waypoints;

    public int direcao;
    public int numNpcs;

    
    void Start()
    {
        StartCoroutine(Spawn());
    }
    
    IEnumerator Spawn()
    {
        for(int i = 0; i < numNpcs; i++)
        {
            Transform child = waypoints.transform.GetChild(Random.Range(0, waypoints.transform.childCount));
            GameObject obj = Instantiate(npcPrefab, child.transform.position, Quaternion.identity, this.transform);
            obj.GetComponent<NavegadorPontos>().pontoAtual = child.GetComponent<Waypoint>();
            NavegadorPontos nav = obj.GetComponent<NavegadorPontos>();
            nav.direcao = Random.Range(0, 2);
            
            yield return new WaitForEndOfFrame();
        }
    }
}
