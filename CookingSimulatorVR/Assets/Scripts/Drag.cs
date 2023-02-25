using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
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
      gameObject.transform.position = Hand.GetPosition();
      gameObject.transform.rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
      gameObject.transform.parent = Hand.GetTransform();
    }
  }

}