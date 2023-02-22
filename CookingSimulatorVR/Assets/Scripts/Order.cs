using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Order : MonoBehaviour
{
  public GlobalVariables.BurgerRecipe burgerRecipe;
  public GlobalVariables.DrinkRecipe drinkRecipe;

  public GameObject orderUI;
  public Canvas ordersListUI;

  // public GameObject красный поднос;

  public int hasBurger;
  public int hasDrink;

  public float orderTime;

  public void GenerateOrder()
  {
    hasBurger = Random.Range(0, 2);
    hasDrink = Random.Range(0, 2);
    orderTime = GlobalVariables.Times["Base"];

    GameObject newOrderUI = Instantiate(orderUI, ordersListUI.transform);

    if (hasBurger == 0 && hasDrink == 0 || hasBurger == 1)
    {
      burgerRecipe = GlobalVariables.BurgerRecipes[Random.Range(0, GlobalVariables.BurgerRecipes.Count)];
      orderTime += GlobalVariables.Times["Beef"];
      newOrderUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = burgerRecipe.name;
    }
    if (hasDrink == 1)
    {
      drinkRecipe = GlobalVariables.DrinkRecipes[Random.Range(0, GlobalVariables.DrinkRecipes.Count)];
      orderTime += GlobalVariables.Times["Drink"];
      newOrderUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = drinkRecipe.name;
    }

    newOrderUI.GetComponent<OrderUI>().StartProgressBar(orderTime, gameObject);
  }

  public void Check()
  {
    // TODO добавить ссылку (public GameObject) на красный поднос,
    // если на подносе бургер и hasBurger == 1 то проверяем порядок ингредиентов в
    // burgerRecipe и бургере на подносе, то же для напитка
    // если все хорошо то заказ удаляем, + деньги, красный поднос очищаем
    // иначе ничего не происходит или всплывает надпись "неправильные ингредиенты" или что то вроде этого
  }
}
