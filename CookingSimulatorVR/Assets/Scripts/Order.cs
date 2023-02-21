using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Order : MonoBehaviour
{
  public GlobalVariables.BurgerRecipe burgerRecipe;
  public GlobalVariables.DrinkRecipe drinkRecipe;

  // для отображения и изменения названий в canvas на объекте
  public TextMeshProUGUI burgerNameText;
  public TextMeshProUGUI drinkNameText;

  // public GameObject красный поднос;

  public int hasBurger;
  public int hasDrink;

  public float orderTime;

  public void GenerateOrder()
  {
    hasBurger = Random.Range(0, 2);
    hasDrink = Random.Range(0, 2);
    orderTime = GlobalVariables.Times["Base"] + GlobalVariables.Times["Beef"] + GlobalVariables.Times["Drink"];

    if (hasBurger == 1)
    {
      burgerRecipe = GlobalVariables.BurgerRecipes[Random.Range(0, GlobalVariables.BurgerRecipes.Count)];
    }
    if (hasDrink == 1)
    {
      drinkRecipe = GlobalVariables.DrinkRecipes[Random.Range(0, GlobalVariables.DrinkRecipes.Count)];
    }

    // TODO добавить UI отображение с прогресс баром и прочим,
    // связать окончание времени с прогрессбаром и жизнью объекта

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
