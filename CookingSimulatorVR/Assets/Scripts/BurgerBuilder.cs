using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GlobalVariables.achievements;

public class BurgerBuilder : MonoBehaviour
{
  private Transform tray;
  [Range(0, 1)] public float value;
  List<Vector3> _line;
  Transform _object_to_move;
  bool canTakeIngredient = true; // canTakeIngredient сетится false, пока ингредиент не "долетел" до места назначения

  int _cheeseNumber = 0;
  void Start()
  {
    tray = transform;
    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { OnPointerDown(); });
    eventTrigger.triggers.Add(pointerDown);

    _line = new List<Vector3>(2);
    _line.Add(Hand.GetPosition()); // точка камеры
    _line.Add(new Vector3());
  }
  void LerpLine()
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
    _object_to_move.rotation = tray.rotation;
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
      _line[0] = Hand.GetPosition();
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
  void OnPointerDown()
  {
    if (!canTakeIngredient)
      return;

    if (!Hand.HasChildren()) // if hand is empty - put the ingredient into it
    {
      if (tray.childCount != 0)
      {
        _object_to_move = tray.GetChild(0);
        _line[1] = _object_to_move.position;
        StartCoroutine(MinusValue());
      }
    }
    else
    {
      string ingredientTag = Hand.GetChildTag();
      // TODO may be need list of possible ingredients
      if (tray.childCount == 0 && ingredientTag == "Bun")
      {
        _object_to_move = Hand.GetTransform().GetChild(0);
        _line[1] = tray.position + new Vector3(0.035f, 0, 0.05f); // костыль
        _object_to_move.transform.rotation = tray.rotation;

        StartCoroutine(PlusValue());
      }
      else if (tray.childCount != 0)
      {
        if (ingredientTag == "Cheese" || ingredientTag == "Tomato" || ingredientTag == "Cooked Beef" || ingredientTag == "Bun" || ingredientTag == "TopBun")
        {
          if (ingredientTag == "Cheese")
          {
            _cheeseNumber++;
            if (_cheeseNumber == 10) AchievementObserver.GetInstance().HandleEvent(cheeseAchiev);
          }
          _object_to_move = Hand.GetTransform().GetChild(0);
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
