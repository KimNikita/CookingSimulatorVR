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

  public void MoveToHand()
  {
    if (!Hand.HasChildren())
    {
      Debug.Log("not working");
      gameObject.transform.position = Hand.GetPosition();
      gameObject.transform.parent = Hand.GetTransform();
    }
  }
}
