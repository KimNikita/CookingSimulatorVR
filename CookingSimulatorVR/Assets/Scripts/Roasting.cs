using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Roasting : MyInteractionManager
{
  Transform stove;

  // поля для анимации переноса
  [Range(0, 1)] public float value;
  List<Vector3> line1 = new List<Vector3>(2);
  Transform beef;
  bool canTakeIngredient = true; // canTakeIngredient сетится false, пока ингредиент не "долетел" до места назначения

  // поля для жарки
  private float timeToFill, startTime;
  public Image progressBarImage;
  public GameObject cookedBeef, burntBeef;

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
    beef.position = Vector3.Lerp(line1[0], line1[1], value);
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
    value = 0;
    canTakeIngredient = true;
    beef.parent = stove;
    beef.rotation = new Quaternion(0, 0, 0, 0);
    timeToFill = GlobalVariables.Times["roastTime"] * (1 - beef.gameObject.GetComponent<Beef>().preparedness);
    StartCoroutine("Fill");
  }

  IEnumerator MinusValue(OculusHand hand)
  {
    canTakeIngredient = false;
    value = 1;
    while (value >= 0)
    {
      yield return new WaitForSeconds(0.01f);
      value -= 0.07f;
      Move();
    }
    beef.rotation = new Quaternion(0, hand.GetRotation().y, 0, hand.GetRotation().w);
    //beef.parent = Hand.GetTransform();
    canTakeIngredient = true;
  }

  void Move()
  {
    LerpLine1();
  }

  void OnPointerDown(OculusHand hand)
  {
    stove = gameObject.transform;

    line1 = new List<Vector3>(2);
    line1.Add(new Vector3());
    line1.Add(new Vector3());

    line1[0] = hand.GetTransform().position; // точка из которой начинается движение
    if (!canTakeIngredient)
      return;

    if (!hand.HasChildren()) // if hand is empty - put the ingredient into it
    {
      if (stove.childCount != 0)
      {
        gameObject.GetComponent<AudioSource>().Stop();
        StopCoroutine("Fill");
        beef.gameObject.GetComponent<Beef>().preparedness = progressBarImage.fillAmount;
        progressBarImage.fillAmount = 0;
        beef = stove.GetChild(0);
        line1[1] = beef.position;
        beef.parent = hand.GetTransform();
        StartCoroutine(MinusValue(hand));
      }
    }
    else // put ingredient onto stove
    {
      string ingredientTag = hand.GetChildTag();
      // TODO may be need list of possible ingredients
      if (stove.childCount == 0 && (ingredientTag == "Beef" || ingredientTag == "Cooked Beef" || ingredientTag == "Burnt Beef"))
      {
        beef = hand.GetChild();
        Vector3 destination = stove.position + new Vector3(0, beef.GetComponent<BoxCollider>().size.y / 2, 0);
        line1[1] = destination;

        StartCoroutine(PlusValue());
      }
    }
  }

  IEnumerator Fill()
  {
    startTime = Time.time;
    float start = 0 + beef.gameObject.GetComponent<Beef>().preparedness;
    float end = 1;

    gameObject.GetComponent<AudioSource>().Play();
    while (progressBarImage.fillAmount < 1)
    {
      progressBarImage.fillAmount = Mathf.Lerp(start, end, (Time.time - startTime) / (timeToFill));
      if (beef.tag == "Beef" && progressBarImage.fillAmount >= (1.0 / 3.0) && progressBarImage.fillAmount <= (2.0 / 3.0))
      {
        // пожареная котлета
        var new_beef_stage = Instantiate(cookedBeef);
        new_beef_stage.transform.parent = beef.transform.parent;
        new_beef_stage.transform.position = beef.transform.position;
        Destroy(beef.gameObject);
        beef = new_beef_stage.transform;
      }
      else if (beef.tag == "Cooked Beef" && progressBarImage.fillAmount >= (2.0 / 3.0) && progressBarImage.fillAmount < 1.0)
      {
        // сгоревшая котлета
        var new_beef_stage = Instantiate(burntBeef);
        new_beef_stage.transform.parent = beef.transform.parent;
        new_beef_stage.transform.position = beef.transform.position;
        Destroy(beef.gameObject);
        beef = new_beef_stage.transform;
      }
      yield return null;
    }
    gameObject.GetComponent<AudioSource>().Stop();
    progressBarImage.fillAmount = 0.0f;
  }
}