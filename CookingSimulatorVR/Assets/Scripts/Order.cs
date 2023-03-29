using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Order : MyInteractionManager
{
  public GlobalVariables.BurgerRecipe burgerRecipe;
  public GlobalVariables.DrinkRecipe drinkRecipe;

  public GameObject orderUI, newOrderUI;

  public AudioClip sound;
  public float volume = 0.5f;

  public int hasBurger;
  public int hasDrink;

  public float orderTime;

  private bool _lolipopWasGiven = false;

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        Drop(leftOculusHand);
      }
      else if (rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        Drop(rightOculusHand);
      }
    }
  }

  public void GenerateOrder(GameObject ordersList, GameObject ordersListUI)
  {
    hasBurger = Random.Range(0, 2);
    hasDrink = Random.Range(0, 2);
    if (hasBurger == 0 && hasDrink == 0)
    {
      hasBurger = 1;
    }
    orderTime = GlobalVariables.Times["Base"];

    newOrderUI = Instantiate(orderUI);
    newOrderUI.transform.SetParent(ordersListUI.transform.GetChild(ordersListUI.transform.childCount - 1), false);
    newOrderUI.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
    newOrderUI.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
    newOrderUI.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
    newOrderUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    newOrderUI.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    newOrderUI.SetActive(false);

    if (hasBurger == 1)
    {
      burgerRecipe = GlobalVariables.BurgerRecipes[Random.Range(0, GlobalVariables.BurgerRecipes.Count)];
      orderTime += GlobalVariables.Times["roastTime"];
      newOrderUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GlobalVariables.Translate[burgerRecipe.name];
      newOrderUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
      gameObject.transform.GetChild(2).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GlobalVariables.Translate[burgerRecipe.name];
      gameObject.transform.GetChild(2).transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.black;

    }
    if (hasDrink == 1)
    {
      drinkRecipe = GlobalVariables.DrinkRecipes[Random.Range(0, GlobalVariables.DrinkRecipes.Count)];
      orderTime += GlobalVariables.Times["Drink"];
      newOrderUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GlobalVariables.Translate[drinkRecipe.name];
      newOrderUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.black;
      gameObject.transform.GetChild(2).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = GlobalVariables.Translate[drinkRecipe.name];
      gameObject.transform.GetChild(2).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.black;
    }

    ordersList.GetComponent<OrdersList>().PlaceOrder(orderTime, ordersListUI, transform, newOrderUI.transform);

    gameObject.GetComponent<AudioSource>().PlayOneShot(sound, volume);

    newOrderUI.SetActive(true);
    newOrderUI.GetComponent<OrderUI>().StartProgressBar(orderTime, gameObject);
  }
  private void Drop(OculusHand hand)
  {
    if (hand.HasChildren())
    {
      if (_lolipopWasGiven == false && hand.GetChildTag() == "Lolipop")
      {
        newOrderUI.GetComponent<OrderUI>().startTime += 4f;
        _lolipopWasGiven = true;
        Destroy(hand.GetChild().gameObject);
      }
    }
  }
}
