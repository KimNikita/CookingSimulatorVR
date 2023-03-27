using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrderPlace : MyInteractionManager
{
  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        PlaceOrder(leftOculusHand);
      }
      else if (rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        PlaceOrder(rightOculusHand);
      }
    }
  }

  void PlaceOrder(OculusHand hand)
  {
    if (hand.HasChildren())
    {
      if (hand.GetChildTag() != "Untagged")
      {
        if (hand.GetChildTag() == "Order")
        {
          if (transform.childCount == 1)
          {
            hand.GetChild().position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            hand.GetChild().rotation = transform.rotation;
            hand.GetChild().parent = transform;
          }
        }
      }
      else
      {
        Debug.LogError("Add tag to object in hand");
      }
    }
  }

}