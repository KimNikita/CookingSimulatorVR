using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TrashBin : MonoBehaviour
{
  public TextMeshProUGUI ScoreText;
  public GameObject interactiveObject;
  public AudioClip sound;
  public float volume = 1;

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
          Destroy(Hand.GetTransform().GetChild(0).gameObject);
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