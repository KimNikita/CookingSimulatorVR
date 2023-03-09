using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Roasting : MonoBehaviour
{
  Transform stove;
  [Range(0, 1)] public float value;
  List<Vector3> line1;
  Transform object1;
  bool canTakeIngredient = true; // canTakeIngredient сетится false, пока ингредиент не "долетел" до места назначения
  void Start()
  {
    stove = gameObject.transform;
    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { OnPointerDown(); });
    eventTrigger.triggers.Add(pointerDown);

    line1 = new List<Vector3>(2);
  }
  void LerpLine1()
  {
    object1.position = Vector3.Lerp(line1[0], line1[1], value);
  }
  IEnumerator PlusValue()
  {
    canTakeIngredient = false;
    while (value <= 1)
    {
      yield return new WaitForSeconds(0.01f);
      value += 0.07f;
      Move();
    }
    if (stove.childCount != 0) object1.parent = stove.GetChild(0).transform;
    else object1.parent = stove;
    object1.transform.rotation = new Quaternion(90, 0, 0, 0);
    canTakeIngredient = true;
    value = 0;
  }
  IEnumerator MinusValue()
  {
    canTakeIngredient = false;
    value = 1;
    while (value >= 0)
    {
      yield return new WaitForSeconds(0.01f);
      value -= 0.07f;
      Move();
    }
    object1.rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
    object1.parent = Hand.GetTransform();
    canTakeIngredient = true;
  }
  void Move()
  {
    LerpLine1();
    DrawLine1();
  }
  void DrawLine1()
  {
    Debug.DrawLine(line1[0], line1[1], Color.red, 0.01f);
  }
  void OnPointerDown()
  {
    if (line1.Count == 0) line1.Add(Hand.GetTransform().position); // точка из которой начинается движение
    if (!canTakeIngredient)
      return;

    if (!Hand.HasChildren()) // if hand is empty - put the ingredient into it
    {
      if (stove.childCount != 0)
      {
        object1 = stove.GetChild(0);
        if (line1.Count != 2) line1.Add(object1.position);
        else line1[1] = object1.position;

        StartCoroutine(MinusValue());
      }
    }
    else // put ingredient onto stove
    {
      Debug.Log("put beef onto stove");
      string ingredientTag = Hand.GetChildTag();
      Debug.Log(ingredientTag);
      // TODO may be need list of possible ingredients
      if (stove.childCount == 0 && (ingredientTag == "Beef" || ingredientTag == "Cooked Beef"))
      {
        object1 = Hand.GetTransform().GetChild(0);
        object1.parent = stove; // это не удалять. так ингредиенты ложатся хотя бы параллельно подносу

        Vector3 destination = stove.position;
        if (line1.Count != 2) line1.Add(destination);
        else line1[1] = destination;
        
        StartCoroutine(PlusValue());
      }
    }
  }
}
