using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Drag : MyInteractionManager
{
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

  void MoveToHand(OculusHand hand)
  {
    if (!hand.HasChildren())
    {
      if (gameObject.tag == "Fanta" || gameObject.tag == "Cola")
      {
        gameObject.transform.position = new Vector3(hand.GetPosition().x, hand.GetPosition().y - 0.25f, hand.GetPosition().z);
        GlobalVariables.hasDrink = false;
      }
      else
      {
        gameObject.transform.position = hand.GetPosition();
      }
      gameObject.transform.rotation = new Quaternion(0, hand.GetRotation().y, 0, hand.GetRotation().w);
      gameObject.transform.parent = hand.GetTransform();
    }
  }
}
