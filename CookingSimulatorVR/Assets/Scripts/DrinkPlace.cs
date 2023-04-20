using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrinkPlace : MonoBehaviour
{
  private Transform tray;
  [Range(0, 1)] public float value;
  List<Vector3> _line;
  Transform _object_to_move;

  void Start()
  {
    tray = gameObject.transform;
    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { OnPointerDown(); });
    eventTrigger.triggers.Add(pointerDown);

    _line = new List<Vector3>(2);
    _line.Add(new Vector3());
    _line.Add(new Vector3());
  }
  void LerpLine()
  {
    _object_to_move.position = Vector3.Lerp(_line[0], _line[1], value);
  }
  IEnumerator PlusValue()
  {
    _object_to_move.rotation = tray.rotation;
    while (value <= 1)
    {
      yield return new WaitForSeconds(0.01f);
      value += 0.07f;
      Move();
    }
    _object_to_move.parent = tray;
    value = 0;
  }
  IEnumerator MinusValue()
  {
    _object_to_move.rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
    value = 1;
    while (value >= 0)
    {
      yield return new WaitForSeconds(0.01f);
      value -= 0.07f;
      _line[0] = Hand.GetPosition() + new Vector3(0, -0.25f, 0);
      Move();
    }
    _object_to_move.parent = Hand.GetTransform();
  }
  void Move()
  {
    LerpLine();
  }
  void OnPointerDown()
  {
    _line[0] = Hand.GetPosition(); // точка из которой начинается движение
    if (!Hand.HasChildren())
    {
      if (tray.childCount == 2)
      {
        _object_to_move = tray.GetChild(1);

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
          _object_to_move = Hand.GetTransform().GetChild(0);
          _line[1] = tray.position + new Vector3(0.033f, -0.19f, -0.3f);

          var triggersList = _object_to_move.GetComponent<EventTrigger>().triggers;
          foreach (var trigger in triggersList)
          {
            if (trigger.eventID == EventTriggerType.PointerDown)
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
