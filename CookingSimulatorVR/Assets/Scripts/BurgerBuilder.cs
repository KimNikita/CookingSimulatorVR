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
      yield return new WaitForSeconds(0.01f);
      value += 0.07f;
      Move();
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
      yield return new WaitForSeconds(0.01f);
      value -= 0.07f;
      Move();
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
      lastIngr = tray.GetChild(0);
    }
    else
    {
      lastIngr = tray.GetChild(0).GetChild(tray.GetChild(0).childCount - 1);
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
    else
    {
      string ingredientTag = hand.GetChildTag();
      // TODO may be need list of possible ingredients
      if (tray.childCount == 0 && ingredientTag == "Bun")
      {
        object1 = hand.GetChild();
        line1[1] = tray.position + new Vector3(0.035f, 0, 0.05f); // костыль
        object1.transform.rotation = tray.rotation;

        StartCoroutine(PlusValue());
      }
      else if (tray.childCount != 0)
      {
        if (ingredientTag == "Cheese" || ingredientTag == "Tomato" || ingredientTag == "Cooked Beef" || ingredientTag == "Bun")
        {
          object1 = hand.GetChild();
          object1.parent = tray; // это не удалять. так ингредиенты ложатся хотя бы параллельно подносу     

          // расчёт точки приземления
          BoxCollider last_ingr_collider = GetLastIngredient().GetComponent<BoxCollider>();
          float last_ingr_y = last_ingr_collider.size.y;

          BoxCollider new_ingr_collider = object1.GetComponent<BoxCollider>();
          float new_ingr_y = new_ingr_collider.size.y;

          Vector3 destination = last_ingr_collider.gameObject.transform.position + new Vector3(0, last_ingr_y / 2f + new_ingr_y / 2f, 0);

          if (line1.Count != 2) line1.Add(destination);
          else line1[1] = destination;

          object1.transform.rotation = tray.GetChild(0).rotation;
          object1.transform.rotation = new Quaternion(0, 90, 0, 0);
          StartCoroutine(PlusValue());

        }
      }
    }
  }
}
