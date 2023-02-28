using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TakeIngredient : MonoBehaviour
{
  public GameObject ingredientPrefab;
  public string ingredientTag;
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
      GameObject instance = Instantiate(ingredientPrefab, Hand.GetPosition(), new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w), Hand.GetTransform());
      instance.tag = ingredientTag;
      instance.AddComponent<BoxCollider>();
    }
  }
}
