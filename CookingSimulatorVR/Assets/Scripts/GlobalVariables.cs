using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
  public static Dictionary<string, int> Costs = new Dictionary<string, int>{
    { "Cheese", 30 },
    { "Tomato", 20 },
    { "Cutlet", 50 },
    { "Fanta", 60 },
    { "Cola", 70 }
  };

  public static int scoreValue = 100;
}
