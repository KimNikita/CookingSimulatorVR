using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TakeIngredient : MonoBehaviour
{
    void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => { MoveToHand(); });
        eventTrigger.triggers.Add(pointerDown);
    }

    void MoveToHand()
    {
        if (!Hand.HasChildren())
        {
            GameObject clone = Object.Instantiate<GameObject>(gameObject);
            clone.transform.position = Hand.GetPosition();
            clone.transform.parent = Hand.GetTransform();
            GameObject.FindGameObjectWithTag("Hand").transform.SetParent(clone.transform);
        }
    }
}
