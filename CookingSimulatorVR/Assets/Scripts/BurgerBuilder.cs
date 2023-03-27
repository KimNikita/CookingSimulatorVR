using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BurgerBuilder : MyInteractionManager
{
  private Transform tray;
  [Range(0, 1)] public float value;
  List<Vector3> line1;
  Transform object1;
  bool canTakeIngredient = true; // canTakeIngredient сетится false, пока ингредиент не "долетел" до места назначения

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        OnPointerDown(leftOculusHand);
      }
      else if (rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        OnPointerDown(rightOculusHand);
      }
    }
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
        _object_to_move.position = Vector3.Lerp(_line[0], _line[1], value);
    }
    if (tray.childCount != 0) object1.parent = tray.GetChild(0).transform;
    else object1.parent = tray;
    object1.transform.rotation = tray.rotation;
    canTakeIngredient = true;
    value = 0;
  }

  IEnumerator MinusValue(OculusHand hand)
  {
    canTakeIngredient = false;
    value = 1;
    while (value >= 0)
    {
        canTakeIngredient = false;
        while (value <= 1)
        {            
            yield return new WaitForSeconds(0.01f);
            value += 0.07f;
            Move();
        }
        if (tray.childCount != 0) _object_to_move.parent = tray.GetChild(0).transform;
        else _object_to_move.parent = tray;
        _object_to_move.transform.rotation = tray.rotation;
        canTakeIngredient = true;
        value = 0;
    }
    object1.rotation = new Quaternion(0, hand.GetRotation().y, 0, hand.GetRotation().w);
    object1.parent = hand.GetTransform();
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

  Transform GetLastIngredient()
  {
    Transform lastIngr = null;
    if (tray.GetChild(0).childCount == 0)
    {
        canTakeIngredient = false;
        value = 1;
        while (value >= 0)
        {
            yield return new WaitForSeconds(0.01f);
            value -= 0.07f;
            _line[0] = Hand.GetTransform().position;
            Move();
        }
        _object_to_move.rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
        _object_to_move.parent = Hand.GetTransform();
        canTakeIngredient = true;
    }
    void Move()
    {
        LerpLine();
    }
    return lastIngr;
  }

  void OnPointerDown(OculusHand hand)
  {
    tray = gameObject.transform;

    line1 = new List<Vector3>(2);
    line1.Add(hand.GetTransform().position); // точка камеры

    if (!canTakeIngredient)
      return;

    if (!hand.HasChildren()) // if hand is empty - put the ingredient into it
    {
      if (tray.childCount != 0)
      {
        object1 = tray.GetChild(0);
        if (line1.Count != 2) line1.Add(object1.position);
        else line1[1] = object1.position;

        StartCoroutine(MinusValue(hand));
      }
    }
  } 
}
