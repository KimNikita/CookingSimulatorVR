using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
  public enum achievements { cheeseAchiev, trashBinAchiev, moneyAchiev, orderAchiev, lolipopAchiev };
  public static Dictionary<string, string> Translate = new Dictionary<string, string>
  {
    { "Cheeseburger", "Чизбургер" },
    { "Gamburger", "Гамбургер" },
    { "Fanta", "Фанта" },
    { "Cola", "Кока-кола" },
    { "Easy", "Легко" },
    { "Medium", "Средне" },
    { "Hard", "Сложно" }
  };

  public static Dictionary<string, int> Costs = new Dictionary<string, int>
  {
    { "Cheese", 20 },
    { "Tomato", 15 },
    { "Burnt Beef", 30},
    { "Cooked Beef", 30 },
    { "Beef", 30 },
    { "Fanta", 15 },
    { "Cola", 20 },
    { "BottomBun", 5},
    { "Bun", 5},
    { "TopBun", 5},
    { "Lolipop", 5},
    { "Tips", 0 },
    { "NDS", 15 }
  };

  public static Dictionary<string, float> Times = new Dictionary<string, float>
  {
    { "BetweenOrders", 20},
    { "Base", 15 },
    { "Drink", 7 },
    { "roastTime", 20 }
  };

  public static Dictionary<string, Vector3> Offsets = new Dictionary<string, Vector3>
  {
    { "Cheese", new Vector3(0, 0.02f, 0) },
    { "Tomato",new Vector3(0, 0.02f, 0) },
    { "Burnt Beef", new Vector3(0, 0, 0)},
    { "Cooked Beef", new Vector3(0, 0, 0) },
    { "Beef", new Vector3(0, 0, 0) },
    { "Fanta", new Vector3(0, -0.15f, 0) },
    { "Cola", new Vector3(0, -0.15f, 0) },
    { "BottomBun", new Vector3(0, 0.02f, 0) },
    { "Bun", new Vector3(0, 0.02f, 0) },
    { "Lolipop", new Vector3(0, 0.04f, 0) },
    { "Order", new Vector3(0, 0, 0) }
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
    { new BurgerRecipe("Cheeseburger", new List<string>{ "BottomBun", "Cheese", "Cooked Beef", "Cheese", "Bun" }) },
    { new BurgerRecipe("Gamburger", new List<string>{ "BottomBun", "Cooked Beef", "Tomato", "Cooked Beef", "Bun" }) }
  };

  public static List<DrinkRecipe> DrinkRecipes = new List<DrinkRecipe>
  {
    { new DrinkRecipe("Fanta") },
    { new DrinkRecipe("Cola") }
  };

  public static bool end = false;
  public static int scoreValue = 100;
  public static bool hasDrink = false;

}
