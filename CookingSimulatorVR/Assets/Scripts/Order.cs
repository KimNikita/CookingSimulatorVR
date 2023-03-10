using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Order : MonoBehaviour
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
  public void GenerateOrder(GameObject ordersListUI)
  {
    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { Drop(); });
    eventTrigger.triggers.Add(pointerDown);

    hasBurger = Random.Range(0, 2);
    hasDrink = Random.Range(0, 2);
    if (hasBurger == 0 && hasDrink == 0)
    {
      hasBurger = 1;
    }
    orderTime = GlobalVariables.Times["Base"];

    // Вариант когда UI нового заказа всегда появляется левее остальных
    // с использованием grid layout
    newOrderUI = Instantiate(orderUI, ordersListUI.transform);

    // Вариант когда UI нового заказа всегда появляется правее остальных
    // Понятия не имею как это замутить
    // GameObject newOrderUI = Instantiate(orderUI, );

    if (hasBurger == 1)
    {
      burgerRecipe = GlobalVariables.BurgerRecipes[Random.Range(0, GlobalVariables.BurgerRecipes.Count)];
      orderTime += GlobalVariables.Times["Beef"];
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

    gameObject.GetComponent<AudioSource>().PlayOneShot(sound, volume);

    newOrderUI.GetComponent<OrderUI>().StartProgressBar(orderTime, gameObject);
  }
  private void Drop()
  {
    if (Hand.HasChildren())
    {
      if (_lolipopWasGiven == false && Hand.GetChildTag() == "Lolipop")
      {
        Debug.Log(newOrderUI.GetComponent<OrderUI>().timeToFill);
        newOrderUI.GetComponent<OrderUI>().startTime += 4f;
        _lolipopWasGiven = true;
        Destroy(Hand.GetTransform().GetChild(0).gameObject);
      }
    }
  }
}
