using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrinkPlace : MonoBehaviour
{
    private Transform tray;
    [Range(0, 1)] public float value;
    List<Vector3> line1;
    Transform object1;

    void Start()
    {
        tray = gameObject.transform;
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => { OnPointerDown(); });
        eventTrigger.triggers.Add(pointerDown);

        line1 = new List<Vector3>(2);
    }
    void LerpLine1()
    {
        object1.position = Vector3.Lerp(line1[0], line1[1], value);
    }
    IEnumerator PlusValue()
    {   
        //object1.transform.rotation = tray.transform.rotation;
        while (value <= 1)
        {
            yield return new WaitForSeconds(0.01f);
            value += 0.07f;
            Move();
        }        
        object1.parent = tray.transform;
        value = 0;
    }
    IEnumerator MinusValue()
    {
        object1.rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
        value = 1;
        while (value >= 0)
        {
            yield return new WaitForSeconds(0.01f);
            value -= 0.07f;
            Move();
        }
        object1.parent = Hand.GetTransform();
    }
    void Move()
    {
        LerpLine1();
        DrawLine1();
    }
    void DrawLine1()
    {
        Debug.DrawLine(line1[0], line1[1], Color.red, 0.01f);
    }
    void OnPointerDown()
    {
        if(line1.Count == 0) line1.Add(Hand.GetTransform().position); // точка из которой начинается движение
        if (!Hand.HasChildren())
        {
            if (tray.childCount == 2)
            {
                object1 = tray.GetChild(1);

                StartCoroutine(MinusValue());
            }
        }
        else
        {
            if (tray.childCount == 1)
            {
                string ingredientTag = Hand.GetChildTag();
                // TODO may be need list of possible drinks
                if (ingredientTag == "Fanta" || ingredientTag == "Cola")
                {
                    object1 = Hand.GetTransform().GetChild(0);
                    line1.Add(tray.transform.position + new Vector3(0.033f, -0.19f, -0.3f));
                    
                    var triggersList = object1.GetComponent<EventTrigger>().triggers;
                    foreach (var trigger in triggersList)
                    {
                        if(trigger.eventID == EventTriggerType.PointerDown)
                        {
                            triggersList.Remove(trigger);
                            break;
                        }
                    }

                    StartCoroutine(PlusValue());
                }
            }
        }
    }
}
