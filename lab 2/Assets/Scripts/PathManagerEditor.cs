using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathManager))]

public class PathManagerEditor : Editor //inherit from editor instead of monobehaviour
{
    [SerializeField]
    PathManager pathManager;

    [SerializeField]
    List<waypoints> thePath;

    List<int> toDelete;
    waypoints selectedPoint = null;
    bool doRepaint = true;


    // Start is called before the first frame update
    private void OnSceneGUI()
    {
        thePath = pathManager.Getpath();
        DrawPath(thePath);
    }

    // Update is called once per frame
    private void OnEnable()
    {
        pathManager = target as PathManager;
        toDelete = new List<int>();
    }

    public override void OnInspectorGUI()
    {
        this.serializedObject.Update();
        thePath = pathManager.Getpath();


        base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("path");

        DrawGUIForPoints();

        if (GUILayout.Button("Add point to path"))
        {
            pathManager.CreatAddPoint();
        }

        EditorGUILayout.EndVertical();
        SceneView.RepaintAll();

    }

    void DrawGUIForPoints()
    {
        if (thePath != null && thePath.Count > 0)
        {
            for (int i = 0; i < thePath.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                waypoints p = thePath[i];

                Color c = GUI.color;
                if (selectedPoint == p) GUI.color = Color.green;

                Vector3 oldPos = p.GetPos();
                Vector3 newPos = EditorGUILayout.Vector3Field("", oldPos);

                if (EditorGUI.EndChangeCheck()) p.SetPos(newPos);

                if (GUILayout.Button("-", GUILayout.Width(35)))
                {
                    toDelete.Add(i);
                }

                GUI.color = c;

                EditorGUILayout.EndHorizontal();
            }
        }
        if (toDelete.Count > 0)
        {
            foreach (int i in toDelete)
                thePath.RemoveAt(i);
            toDelete.Clear();
        }
    }

    public void DrawPath(List<waypoints> path)
    {
        if (path != null)
        {
            int current = 0;
            foreach (waypoints wp in path)
            {
                doRepaint = DrawPoint(wp);
                int next = (current + 1) % path.Count;
                waypoints wpnext = path[next];

                DrawPathLine(wp, wpnext);
                current += 1;
            }
            if (doRepaint) Repaint();
        }
    }

    public void DrawPathLine(waypoints p1, waypoints p2)
    {
        Color c = Handles.color;
        Handles.color = Color.grey;
        Handles.DrawLine(p1.GetPos(), p2.GetPos());
        Handles.color = c;


    }

    public bool DrawPoint(waypoints p)
    {
        bool isChanged = false;

        if (selectedPoint == p)
        {
            Color c = Handles.color;
            Handles.color = Color.green;

            EditorGUI.BeginChangeCheck();
            Vector3 oldpos = p.GetPos();
            Vector3 newpos = Handles.PositionHandle(oldpos, Quaternion.identity);

            float handleSize = HandleUtility.GetHandleSize(newpos);
            Handles.SphereHandleCap(-1, newpos, Quaternion.identity, 0.25f * handleSize, EventType.Repaint);
            if (EditorGUI.EndChangeCheck())
            {
                p.SetPos(newpos);
            }
            Handles.color = c;
        }
        else
        {
            Vector3 currPos = p.GetPos();
            float handleSize = HandleUtility.GetHandleSize(currPos);
            if (Handles.Button(currPos, Quaternion.identity, 0.25f * handleSize, 0.25f * handleSize, Handles.SphereHandleCap))
            {
                isChanged = true;
                selectedPoint = p;
            }
        }

        return isChanged;

    }

}
