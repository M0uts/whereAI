using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointManager : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointManager>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if(waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DesenharBotao();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    void DesenharBotao()
    {
        if(GUILayout.Button("Criar Waypoint"))
        {
            CriarWaypoint();
        }
        if(Selection.activeGameObject !=null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if(GUILayout.Button("Adicionar Branch (Ramo)"))
            {
                CriarBranch();
            }
            if(GUILayout.Button("Criar Waypoint Antes"))
            {
                CriarWaypointAntes();
            }
            if (GUILayout.Button("Criar Waypoint Depois"))
            {
                CriarWaypointDepois();
            }
            if (GUILayout.Button("Remover Waypoint"))
            {
                RemoverWaypoint();
            }
        }
    }

    void CriarWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if(waypointRoot.childCount > 1)
        {
            waypoint.pontoAnterior = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.pontoAnterior.pontoSeguinte = waypoint;
            //coloca o ponto na ultima posição
            waypoint.transform.position = waypoint.pontoAnterior.transform.position;
            waypoint.transform.forward = waypoint.pontoAnterior.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObject;
    }

    void CriarWaypointAntes()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.transform.forward;

        if(selectedWaypoint.pontoAnterior != null)
        {
            newWaypoint.pontoAnterior = selectedWaypoint.pontoAnterior;
            selectedWaypoint.pontoAnterior.pontoSeguinte = newWaypoint;
        }

        newWaypoint.pontoSeguinte = selectedWaypoint;
        selectedWaypoint.pontoAnterior = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void CriarWaypointDepois()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.transform.forward;

        newWaypoint.pontoAnterior = selectedWaypoint;

        if(selectedWaypoint.pontoSeguinte != null)
        {
            selectedWaypoint.pontoSeguinte.pontoAnterior = newWaypoint;
            newWaypoint.pontoSeguinte = selectedWaypoint.pontoSeguinte;
        }

        selectedWaypoint.pontoSeguinte = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void RemoverWaypoint()
    {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        if(selectedWaypoint.pontoSeguinte != null)
        {
            selectedWaypoint.pontoSeguinte.pontoAnterior = selectedWaypoint.pontoAnterior;
        }
        if(selectedWaypoint.pontoAnterior != null)
        {
            selectedWaypoint.pontoAnterior.pontoSeguinte = selectedWaypoint.pontoSeguinte;
            Selection.activeGameObject = selectedWaypoint.pontoAnterior.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);
    }

    void CriarBranch()
    {
        GameObject waypointObject = new GameObject("Branch " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();
        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }
}
