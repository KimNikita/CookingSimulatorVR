using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TakeIngredient : MyInteractionManager
{

  public GameObject ingredientPrefab;
  public string ingredientTag;

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

  public void MoveToHand(OculusHand hand)
  {
    if (!hand.HasChildren())
    {
      GameObject instance = Instantiate(ingredientPrefab, hand.GetPosition(), new Quaternion(0, hand.GetRotation().y, 0, hand.GetRotation().w), hand.GetTransform());
      instance.tag = ingredientTag;
      instance.AddComponent<BoxCollider>();
    }
}
