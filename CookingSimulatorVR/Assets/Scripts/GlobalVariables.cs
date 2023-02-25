using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
  public static Dictionary<string, int> Costs = new Dictionary<string, int>
  {
    { "Cheese", 30 },
    { "Tomato", 20 },
    { "Beef", 50 },
    { "Fanta", 60 },
    { "Cola", 70 }
  };

  public static Dictionary<string, float> Times = new Dictionary<string, float>
  {
    { "BetweenOrders", 20},
    { "Base", 10 },
    { "Drink", 7 },
    { "Beef", 10 }
  };

  public struct BurgerRecipe
  {
    public string name;
    public List<string> ingredients;
    public BurgerRecipe(string _name, List<string> _ingredients)
    {
      this.name = _name;
      this.ingredients = _ingredients;
    }
  }

  public struct DrinkRecipe
  {
    public string name;
    public DrinkRecipe(string _name)
    {
      this.name = _name;
    }
  }

  public static List<BurgerRecipe> BurgerRecipes = new List<BurgerRecipe>
  {
    { new BurgerRecipe("Burger name 1", new List<string>{ "ingredient 1", "ingredient 2" }) },
    { new BurgerRecipe("Burger name 2", new List<string>{ "ingredient 1", "ingredient 2" }) }
  };

  public static List<DrinkRecipe> DrinkRecipes = new List<DrinkRecipe>
  {
    { new DrinkRecipe("Drink name 1") },
    { new DrinkRecipe("Drink name 2") }
  };

  public static bool end = false;
  public static int scoreValue = 100;
  public static float timeBetweenOrders = 20;
  public static bool hasDrink = false;

}
