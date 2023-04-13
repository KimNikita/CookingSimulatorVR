using System.Collections;
using UnityEngine;
using TMPro;
using static GlobalVariables;
using static GlobalVariables.achievements;

public class Order : MyInteractionManager
{
  public BurgerRecipe burgerRecipe;
  public DrinkRecipe drinkRecipe;

  public GameObject orderUI, newOrderUI;
  public GameObject person;

  public AudioClip sound;
  public float volume = 0.5f;

  public int hasBurger;
  public int hasDrink;

  public float orderTime;

  private bool _lolipopWasGiven = false;
  static int _lolipop_num = 0;

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

  public void GenerateOrder(GameObject ordersList, GameObject ordersListUI, GameObject _person)
  {
    person = _person;
    hasBurger = Random.Range(0, 2);
    hasDrink = Random.Range(0, 2);
    if (hasBurger == 0 && hasDrink == 0)
    {
      hasBurger = 1;
    }
    orderTime = Times["Base"];

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
      burgerRecipe = BurgerRecipes[Random.Range(0, BurgerRecipes.Count)];
      orderTime += Times["roastTime"];
      newOrderUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Translate[burgerRecipe.name];
      newOrderUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
      gameObject.transform.GetChild(2).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Translate[burgerRecipe.name];
      gameObject.transform.GetChild(2).transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.black;

    }
    if (hasDrink == 1)
    {
      drinkRecipe = DrinkRecipes[Random.Range(0, DrinkRecipes.Count)];
      orderTime += Times["Drink"];
      newOrderUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Translate[drinkRecipe.name];
      newOrderUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.black;
      gameObject.transform.GetChild(2).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = Translate[drinkRecipe.name];
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

        _lolipop_num++;
        if (_lolipop_num == 10) AchievementManager.GetInstance().HandleEvent(lolipopAchiev);
      }
    }
    else
    {
      transform.position = hand.GetPosition() + Offsets[gameObject.tag];
      transform.rotation = hand.GetRotation();
      transform.parent = hand.GetTransform();
    }
  }
}
