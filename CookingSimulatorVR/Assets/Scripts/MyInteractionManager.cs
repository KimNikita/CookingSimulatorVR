using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MyInteractionManager : XRSimpleInteractable
{
  public InputActionProperty leftController, rightController;

  protected OculusHand leftOculusHand, rightOculusHand;

  protected virtual void Start()
  {
    interactionManager = GameObject.FindGameObjectWithTag("XR Interaction Manager").GetComponent<XRInteractionManager>();
    leftOculusHand = GameObject.FindGameObjectWithTag("LeftHand Controller").GetComponent<OculusHand>();
    rightOculusHand = GameObject.FindGameObjectWithTag("RightHand Controller").GetComponent<OculusHand>();
  }

  public void StartCheck()
  {
    StartCoroutine("Check");
  }

  public void StopCheck()
  {
    StopCoroutine("Check");
  }

  virtual protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        Debug.Log("Left Hand Activate Event");
      }
      else if (rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        Debug.Log("Right Hand Activate Event");
      }
    }
  }
}
