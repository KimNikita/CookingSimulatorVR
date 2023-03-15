using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static Dictionary<string, string> Translate = new Dictionary<string, string>
  {
    { "Cheeseburger", "Чизбургер" },
    { "Gamburger", "Гамбургер" },
    { "Fanta", "Фанта" },
    { "Cola", "Кока-кола" }
  };

    public static Dictionary<string, int> Costs = new Dictionary<string, int>
  {
    { "Beef", 30 },
    { "Bun", 5},
    { "Burnt Beef", 30 },
    { "Cheese", 20 },
    { "Cooked Beef", 30 },
    { "Cola", 20 },
    { "Fanta", 15 },
    { "Lolipop", 5},
    { "NDS", 15 },
    { "Tips", 0 },    
    { "Tomato", 15 }
  };

    public static Dictionary<string, float> Times = new Dictionary<string, float>
  {
    { "Base", 15 },
    { "Beef", 10 },
    { "BetweenOrders", 15},    
    { "Drink", 7 }
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
    { new BurgerRecipe("Cheeseburger", new List<string>{ "Bun", "Cheese", "Beef", "Cheese", "Bun" }) },
    { new BurgerRecipe("Gamburger", new List<string>{ "Bun", "Beef", "Tomato", "Beef", "Bun" }) }
  };

    public static List<DrinkRecipe> DrinkRecipes = new List<DrinkRecipe>
  {
    { new DrinkRecipe("Fanta") },
    { new DrinkRecipe("Cola") }
  };


    public static bool end = false;
    public static int scoreValue = 100;
    public static float timeBetweenOrders = 20;
    public static bool hasDrink = false;

}
