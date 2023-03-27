using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrinkPlace : MyInteractionManager
{
  private Transform tray;
  [Range(0, 1)] public float value;
  List<Vector3> line1;
  Transform object1;

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
    object1.transform.rotation = tray.transform.rotation;
    while (value <= 1)
    {
      yield return new WaitForSeconds(0.01f);
      value += 0.07f;
      Move();
    }
    object1.parent = tray.transform;
    value = 0;
  }

  IEnumerator MinusValue(OculusHand hand)
  {
    object1.rotation = new Quaternion(0, hand.GetRotation().y, 0, hand.GetRotation().w);
    value = 1;
    while (value >= 0)
    {
      yield return new WaitForSeconds(0.01f);
      value -= 0.07f;
      Move();
    }
    object1.parent = hand.GetTransform();
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

  void OnPointerDown(OculusHand hand)
  {
    tray = gameObject.transform;
    line1 = new List<Vector3>(2);

    if (line1.Count == 0) line1.Add(hand.GetTransform().position); // точка из которой начинается движение
    if (!hand.HasChildren())
    {
      if (tray.childCount == 2)
      {
        object1 = tray.GetChild(1);

        StartCoroutine(MinusValue(hand));
      }
    }
    else
    {
      if (tray.childCount == 1)
      {
        string ingredientTag = hand.GetChildTag();
        // TODO may be need list of possible drinks
        if (ingredientTag == "Fanta" || ingredientTag == "Cola")
        {
          object1 = hand.GetChild();
          line1.Add(tray.transform.position + new Vector3(0.033f, -0.19f, -0.3f));

          var triggersList = object1.GetComponent<EventTrigger>().triggers;
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
