using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GlobalVariables;
using static GlobalVariables.achievements;

public class BurgerBuilder : MyInteractionManager
{
  private Transform tray;
  [Range(0, 1)] public float value;
  List<Vector3> _line;
  Transform _object_to_move;
  bool canTakeIngredient = true; // canTakeIngredient сетится false, пока ингредиент не "долетел" до места назначения
  int _cheeseNumber = 0;

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
    _object_to_move.position = Vector3.Lerp(_line[0], _line[1], value);
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
    if (tray.childCount != 0) _object_to_move.parent = tray.GetChild(0).transform;
    else _object_to_move.parent = tray;
    _object_to_move.transform.rotation = tray.rotation;
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
      _line[0] = hand.GetTransform().position + Offsets[_object_to_move.tag];
      Move();
    }
    _object_to_move.rotation = new Quaternion(0, hand.GetRotation().y, 0, hand.GetRotation().w);
    _object_to_move.parent = hand.GetTransform();
    canTakeIngredient = true;
  }

  void Move()
  {
    LerpLine1();
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

    _line = new List<Vector3>(2);
    _line.Add(hand.GetTransform().position); // точка камеры
    _line.Add(new Vector3());

    if (!canTakeIngredient)
      return;

    if (!hand.HasChildren()) // if hand is empty - put the ingredient into it
    {
      if (tray.childCount != 0)
      {
        _object_to_move = tray.GetChild(0);
        _line[1] = _object_to_move.position;
        StartCoroutine(MinusValue(hand));

        StartCoroutine(MinusValue(hand));
      }
    }
    else
    {
      string ingredientTag = hand.GetChildTag();
      // TODO may be need list of possible ingredients
      if (tray.childCount == 0 && ingredientTag == "BottomBun")
      {
        _object_to_move = hand.GetChild();
        _line[1] = tray.position + new Vector3(0.035f, 0, 0.05f); // костыль
        _object_to_move.transform.rotation = tray.rotation;

        StartCoroutine(PlusValue());
      }
      else if (tray.childCount != 0)
      {
        if (ingredientTag == "Cheese" || ingredientTag == "Tomato" || ingredientTag == "Cooked Beef" || ingredientTag == "Bun")
        {
          if (ingredientTag == "Cheese")
          {
            _cheeseNumber++;
            if (_cheeseNumber == 10) AchievementManager.GetInstance().HandleEvent(cheeseAchiev);
          }

          _object_to_move = hand.GetChild();
          _object_to_move.parent = tray; // это не удалять. так ингредиенты ложатся хотя бы параллельно подносу     

          // расчёт точки приземления
          BoxCollider last_ingr_collider = GetLastIngredient().GetComponent<BoxCollider>();
          float last_ingr_y = last_ingr_collider.size.y;

          BoxCollider new_ingr_collider = _object_to_move.GetComponent<BoxCollider>();
          float new_ingr_y = new_ingr_collider.size.y;

          Vector3 destination = last_ingr_collider.gameObject.transform.position + new Vector3(0, last_ingr_y / 2f + new_ingr_y / 2f, 0);

          _line[1] = destination;

          _object_to_move.transform.rotation = tray.GetChild(0).rotation;
          _object_to_move.transform.rotation = new Quaternion(0, 90, 0, 0);
          StartCoroutine(PlusValue());

        }
      }
    }
  }
}
