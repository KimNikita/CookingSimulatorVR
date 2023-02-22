using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveBunToTray : MonoBehaviour
{
    [Range(0, 1)] public float value;
    List<Transform> line;
    int countLine;
    Vector3 point1;
    public GameObject objectToMove;
    public Transform parentLine;
    public Transform tray;
    public void StartMoving()
    {
        if(tray.transform.childCount == 0)
        {
            objectToMove = Object.Instantiate(objectToMove);
            line = new List<Transform>();
            RefreshLine();
            value = 0;
            StartCoroutine(PlusValue());
        }
    }    
    void RefreshLine()
    {
        parentLine.GetComponentsInChildren<Transform>(line);
        countLine = line.Count;
    }
    void LerpLine()
    {
        List<Vector3> list = new List<Vector3>();
        for (int i = 1; i < line.Count - 1; i++)
        {
            list.Add(Vector3.Lerp(line[i].position, line[i + 1].position, value));
        }
        Lerp2Line(list);
    }
    void Lerp2Line(List<Vector3> list2)
    {
        if (list2.Count > 2)
        {
            List<Vector3> list = new List<Vector3>();
            for (int i = 0; i < list2.Count - 1; i++)
            {
                list.Add(Vector3.Lerp(list2[i], list2[i + 1], value));
            }
            Lerp2Line(list);
        }
        else
        {
            objectToMove.transform.position = Vector3.Lerp(list2[0], list2[1], value);
        }
    }
    IEnumerator PlusValue()
    {
        while (value < 1)
        {
            yield return new WaitForSeconds(0.01f);
            value += 0.01f;
            Move();
        }
        objectToMove.transform.parent = tray;
    }
    void Move()
    {
        if (parentLine.childCount != countLine - 1)
        {
            RefreshLine();
        }
        LerpLine();
        DrawLine();
    }
    
    void DrawLine()
    {
        point1 = line[1].position;
        for (float t = 0; t < 1; t += 0.01f)
        {
            List<Vector3> list = new List<Vector3>();
            for (int i = 1; i < line.Count - 1; i++)
            {
                list.Add(Vector3.Lerp(line[i].position, line[i + 1].position, t));
            }
            Draw2Line(list, t);
        }
    }
    void Draw2Line(List<Vector3> list2, float value4)
    {
        if (list2.Count > 2)
        {
            List<Vector3> list = new List<Vector3>();
            for (int i = 0; i < list2.Count - 1; i++)
            {
                list.Add(Vector3.Lerp(list2[i], list2[i + 1], value4));
            }
            Draw2Line(list, value4);
        }
        else
        {
            Vector3 point2 = Vector3.Lerp(list2[0], list2[1], value4);
            Debug.DrawLine(point1, point2, Color.red, 0.01f);
            point1 = point2;
        }
    }
}
