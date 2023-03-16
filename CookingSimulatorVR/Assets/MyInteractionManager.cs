using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class MyInteractionManager : XRSimpleInteractable
{
  public InputActionProperty rightController;
  public GameObject rightHand, leftHand;
 
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (!rightHand.GetComponent<Hand>().HasChildren() && rightController.action.ReadValue<float>() > 0.1)
    {
      Debug.Log("sadgashd");
      
    }
  }
}
