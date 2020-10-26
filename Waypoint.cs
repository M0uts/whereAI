using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint pontoAnterior;
    public Waypoint pontoSeguinte;

    [Range(0f, 20f)]
    public float width = 10f;

    public List<Waypoint> branches = new List<Waypoint>();

    [Range(0f, 1f)]
    public float branchRatio = 0.5f;

    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 5f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }
}
