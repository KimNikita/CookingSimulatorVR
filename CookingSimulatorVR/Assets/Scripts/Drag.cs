using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static GlobalVariables;

public class Drag : MyInteractionManager
{
  [Range(0, 1)] public float value;
  List<Vector3> _line;
  Transform _object_to_move;
  bool canTakeIngredient = true; // canTakeIngredient сетится false, пока ингредиент не "долетел" до места назначения

  protected override void Start()
  {
    base.Start();
    _line = new List<Vector3>(2);
    _line.Add(new Vector3());
    _line.Add(transform.position);
  }

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        MoveToHand(leftOculusHand);
      }
      else if (rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        MoveToHand(rightOculusHand);
      }
    }
  }

  void LerpLine()
  {
    _object_to_move.position = Vector3.Lerp(_line[0], _line[1], value);
  }
  void Move()
  {
    LerpLine();
  }
  IEnumerator MinusValue(OculusHand hand)
  {
    canTakeIngredient = false;
    value = 1;
    while (value >= 0)
    {
      yield return new WaitForSeconds(0.01f);
      _line[0] = hand.GetPosition() + Offsets[_object_to_move.tag];
      value -= 0.07f;
      Move();
    }
    canTakeIngredient = true;
    _object_to_move.transform.rotation = new Quaternion(0, hand.GetRotation().y, 0, hand.GetRotation().w);
    _object_to_move.transform.parent = hand.GetTransform();
  }

  public void MoveToHand(OculusHand hand)
  {
    if (!hand.HasChildren())
    {
      _object_to_move = gameObject.transform;
      if (gameObject.tag == "Fanta" || gameObject.tag == "Cola")
      {
        hasDrink = false;
      }
      StartCoroutine(MinusValue(hand));
    }
  }
}
