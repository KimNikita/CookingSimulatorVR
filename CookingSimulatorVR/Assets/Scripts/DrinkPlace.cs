using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using static GlobalVariables;

public class DrinkPlace : MyInteractionManager
{
  private Transform tray;
  [Range(0, 1)] public float value;
  List<Vector3> _line;
  Transform _object_to_move;

  protected override void Start()
  {
    base.Start();
    tray = gameObject.transform;
    _line = new List<Vector3>(2)
    {
      new Vector3(),
      new Vector3()
    };
  }

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
    _object_to_move.transform.rotation = tray.transform.rotation;
    while (value <= 1)
    {
      yield return new WaitForSeconds(0.01f);
      value += 0.07f;
      Move();
    }
    _object_to_move.parent = tray.transform;
    value = 0;
  }

  IEnumerator MinusValue(OculusHand hand)
  {
    _object_to_move.rotation = new Quaternion(0, hand.GetRotation().y, 0, hand.GetRotation().w);
    value = 1;
    while (value >= 0)
    {
      yield return new WaitForSeconds(0.01f);
      value -= 0.07f;
      _line[0] = hand.GetTransform().position + Offsets[_object_to_move.tag];
      Move();
    }
    _object_to_move.parent = hand.GetTransform();
  }

  void Move()
  {
    LerpLine1();
  }


  void OnPointerDown(OculusHand hand)
  {
    _line[0] = hand.GetPosition(); // точка из которой начинается движение

    if (!hand.HasChildren())
    {
      if (tray.childCount == 2)
      {
        _object_to_move = tray.GetChild(1);

        StartCoroutine(MinusValue(hand));
      }
    }
    else
    {
      if (tray.childCount == 1)
      {
        string ingredientTag = hand.GetChildTag();
        if (ingredientTag == "Fanta" || ingredientTag == "Cola")
        {
          _object_to_move = hand.GetChild();
          _line[1] = tray.transform.position + new Vector3(0.033f, -0.19f, -0.3f); // костыль

          StartCoroutine(PlusValue());
        }
      }
    }
  }
}
