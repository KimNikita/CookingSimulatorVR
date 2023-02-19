using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TrashBin : MonoBehaviour
{
  public TextMeshProUGUI ScoreText;
  public GameObject interactiveObject;

  void Start()
  {
    EventTrigger eventTrigger = interactiveObject.AddComponent<EventTrigger>();

    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { DropIn(); });

    eventTrigger.triggers.Add(pointerDown);
  }

  void DropIn()
  {
    if (Hand.HasChildren())
    {
      GlobalVariables.scoreValue -= GlobalVariables.Costs[Hand.GetChildTag()];
      ScoreText.text = GlobalVariables.scoreValue + "$";
      Destroy(Hand.GetTransform().GetChild(0));
    }
  }

}