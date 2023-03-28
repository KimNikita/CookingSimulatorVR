using UnityEngine;
using static GlobalVariables;

public class AchievementObserver : MonoBehaviour, Observer
{
    public void HandleEvent(achievements ach)
    {
        // здесь обрабатывать события: выводить что-нибудь на UI и т.д.
        if (ach == achievements.cheeseAchiev)
        {
            Debug.Log("Cheese Achievement was gotten");
        }
        if (ach == achievements.trashBinAchiev)
        {
            Debug.Log("Trash bin Achievement was gotten");
        }
        if (ach == achievements.moneyAchiev)
        {
            Debug.Log("Money Achievement was gotten");
        }
        if (ach == achievements.orderAchiev)
        {
            Debug.Log("Order Achievement was gotten");
        }
        if (ach == achievements.lolipopAchiev)
        {
            Debug.Log("Lolipop Achievement was gotten");
        }
    }
}
