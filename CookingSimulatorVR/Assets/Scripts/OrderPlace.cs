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
          if (transform.childCount == 1)
          {
            Hand.GetTransform().GetChild(0).position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            Hand.GetTransform().GetChild(0).rotation = transform.rotation;
            Hand.GetTransform().GetChild(0).parent = transform;
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