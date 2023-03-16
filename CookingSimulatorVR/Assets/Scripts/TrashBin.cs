﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TrashBin : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public GameObject interactiveObject;

    [Range(0, 1)] public float value;
    List<Vector3> line1;
    Transform object1;

    public AudioClip sound;
    public float volume = 1;

    void Start()
    {
        EventTrigger eventTrigger = interactiveObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => { DropIn(); });

        eventTrigger.triggers.Add(pointerDown);

        line1 = new List<Vector3>(2);
        line1.Add(Hand.GetTransform().position); // точка камеры
        line1.Add(interactiveObject.transform.position + new Vector3(0.3f, 0.3f, 0f));
        line1.Add(interactiveObject.transform.position);
        line1.Add(interactiveObject.transform.position + new Vector3(0, -1f, -0.2f));
    }

    void LerpLine()
    {
        List<Vector3> list = new List<Vector3>();
        for (int i = 0; i < line1.Count - 1; i++)
        {
            list.Add(Vector3.Lerp(line1[i], line1[i + 1], value));
        }
        Lerp2Line(list);
    }
    IEnumerator PlusValue()
    {
        while (value <= 1)
        {
            yield return new WaitForSeconds(0.01f);
            value += 0.03f;
            Move();
        }
        value = 0;
        Destroy(object1.gameObject);
    }
    void Move()
    {
        LerpLine();
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
            object1.position = Vector3.Lerp(list2[0], list2[1], value);
        }
    }
    void DropIn()
    {
        if (Hand.HasChildren())
        {
            if (Hand.GetChildTag() != "Untagged")
            {
                if (GlobalVariables.Costs.ContainsKey(Hand.GetChildTag()))
                {
                    int childCount = Hand.GetTransform().GetChild(0).childCount;
                    if (childCount != 0)
                    {
                        for (int i = 0; i < childCount; i++)
                        {
                            GlobalVariables.scoreValue -= GlobalVariables.Costs[Hand.GetTransform().GetChild(0).GetChild(i).tag];
                        }
                    }
                    GlobalVariables.scoreValue -= GlobalVariables.Costs[Hand.GetChildTag()];
                    ScoreText.text = GlobalVariables.scoreValue + "$";
                    line1[0] = Hand.GetTransform().position;
                    object1 = Hand.GetTransform().GetChild(0);
                    StartCoroutine(PlusValue());
                    gameObject.GetComponent<AudioSource>().PlayOneShot(sound, volume);
                }
                else if (Hand.GetChildTag() != "Order")
                {
                    Debug.LogError("Unknown tag of object in hand");
                }
            }
            else
            {
                Debug.LogError("Add tag to object in hand");
            }
        }
    }

}