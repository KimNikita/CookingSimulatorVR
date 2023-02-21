using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderPlace : MonoBehaviour
{

  void Start()
  {
    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { PlaceOrder(); });

    eventTrigger.triggers.Add(pointerDown);
  }

  void PlaceOrder()
  {
    if (Hand.HasChildren())
    {
      if (Hand.GetChildTag() != "Untagged")
      {
        if (Hand.GetChildTag() == "Order")
        {
          if (gameObject.transform.childCount == 0)
          {
            Hand.GetTransform().GetChild(0).position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f, gameObject.transform.position.z);
            Hand.GetTransform().GetChild(0).parent = gameObject.transform;
          }
        }
      }
      else
      {
        Debug.LogError("Add tag to object in hand");
      }
    }
  }

}