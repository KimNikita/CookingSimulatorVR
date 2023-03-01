using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrinkPlace : MonoBehaviour
{
  private Transform tray;

  void Start()
  {
    tray = gameObject.transform;
    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { OnPointerDown(); });
    eventTrigger.triggers.Add(pointerDown);
  }
  void OnPointerDown()
  {
    if (!Hand.HasChildren())
    {
      if (tray.childCount == 2)
      {
        tray.GetChild(1).rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
        tray.GetChild(1).transform.position = Hand.GetPosition();
        tray.GetChild(1).transform.parent = Hand.GetTransform();
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
          Transform drink = Hand.GetTransform().GetChild(0);
          drink.transform.position = tray.transform.position + new Vector3(0.033f, -0.19f, -0.3f); // костыль
          drink.transform.rotation = tray.transform.rotation;
          drink.parent = tray.transform;
        }
      }
    }
  }
}
