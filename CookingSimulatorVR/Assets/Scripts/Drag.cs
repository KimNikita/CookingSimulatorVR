using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GlobalVariables;

public class Drag : MonoBehaviour
{
  [Range(0, 1)] public float value;
  List<Vector3> _line;
  Transform _object_to_move;
  bool canTakeIngredient = true; // canTakeIngredient сетится false, пока ингредиент не "долетел" до места назначения
  void Start()
  {
    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { MoveToHand(); });
    eventTrigger.triggers.Add(pointerDown);

    _line = new List<Vector3>(2);
    _line.Add(new Vector3());
    _line.Add(new Vector3());
    _line[0] = Hand.GetPosition();
    _line[1] = transform.position;
  }
  void LerpLine()
  {
    _object_to_move.position = Vector3.Lerp(_line[0], _line[1], value);
  }
  void Move()
  {
    LerpLine();
  }
  IEnumerator MinusValue()
  {
    canTakeIngredient = false;
    value = 1;
    while (value >= 0)
    {
      yield return new WaitForSeconds(0.01f);
      _line[0] = Hand.GetPosition();
      if (gameObject.tag == "Fanta" || gameObject.tag == "Cola")
      {
        _line[0] += new Vector3(0, -0.25f, 0);
      }
      value -= 0.07f;
      Move();
    }
    canTakeIngredient = true;
    _object_to_move.rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
    _object_to_move.parent = Hand.GetTransform();
  }
  void MoveToHand()
  {

    if (!Hand.HasChildren())
    {
      _object_to_move = gameObject.transform;
      if (gameObject.tag == "Fanta" || gameObject.tag == "Cola")
      {
        hasDrink = false;
      }
      StartCoroutine(MinusValue());
    }
  }
}
