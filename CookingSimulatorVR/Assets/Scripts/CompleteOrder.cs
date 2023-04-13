using System.Collections;
using UnityEngine;
using TMPro;
using static GlobalVariables;
using static GlobalVariables.achievements;

public class CompleteOrder : MyInteractionManager
{
  public TextMeshProUGUI ScoreText;
  public GameObject tray;
  public AudioClip right;
  public AudioClip wrong;
  public float volume = 1;

  static int _orders_number = 0;

  public TextMeshProUGUI CashBoxText;

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        Complete(leftOculusHand);
      }
      else if (rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        Complete(rightOculusHand);
      }
    }
  }

  void Complete(OculusHand hand)
  {
    if (hand.HasChildren())
    {
      if (hand.GetChildTag() == "Order")
      {
        int money = Costs["Bun"];
        Order order = hand.GetChild().GetComponent<Order>();
        if (order.hasBurger != 0)
        {
          if (tray.transform.GetChild(2).childCount != 0 && tray.transform.GetChild(2).GetChild(0).childCount + 1 == order.burgerRecipe.ingredients.Count)
          {
            Transform burger = tray.transform.GetChild(2).GetChild(0);
            for (int i = 0; i < burger.childCount; i++)
            {
              if (burger.GetChild(i).tag == order.burgerRecipe.ingredients[i + 1])
              {
                money += Costs[order.burgerRecipe.ingredients[i + 1]] + Costs["NDS"] +Costs["Tips"];
              }
              else
              {
                Debug.Log("Wrong ingredient");
                gameObject.GetComponent<AudioSource>().PlayOneShot(wrong, volume);
                return;
              }
            }
          }
          else
          {
            Debug.Log("Wrong num of ingredients");
            gameObject.GetComponent<AudioSource>().PlayOneShot(wrong, volume);
            return;
          }
        }
        else if (tray.transform.GetChild(2).childCount != 0)
        {
          Debug.Log("burger missing");
          gameObject.GetComponent<AudioSource>().PlayOneShot(wrong, volume);
          return;
        }

        if (order.hasDrink != 0)
        {
          if (tray.transform.GetChild(0).childCount == 2 && order.drinkRecipe.name == tray.transform.GetChild(0).GetChild(1).tag)
          {
            money += Costs[order.drinkRecipe.name] + Costs["NDS"] + Costs["Tips"];
          }
          else
          {
            gameObject.GetComponent<AudioSource>().PlayOneShot(wrong, volume);
            return;
          }
        }
        else if (tray.transform.GetChild(0).childCount == 2)
        {
          gameObject.GetComponent<AudioSource>().PlayOneShot(wrong, volume);
          return;
        }

        scoreValue += money;
        ScoreText.text = scoreValue + "$";
        gameObject.GetComponent<AudioSource>().PlayOneShot(right, volume);
        StartCoroutine(Cash_appear(money));

        _orders_number++;
        if (_orders_number == 10) AchievementManager.GetInstance().HandleEvent(orderAchiev);
        if ((scoreValue - money) < 1000 && scoreValue >= 1000) AchievementManager.GetInstance().HandleEvent(moneyAchiev);

        if (tray.transform.GetChild(2).childCount != 0)
        {
          Destroy(tray.transform.GetChild(2).GetChild(0).gameObject);
        }
        if (tray.transform.GetChild(0).childCount == 2)
        {
          Destroy(tray.transform.GetChild(0).GetChild(1).gameObject);
        }
        if (order.orderUI != null)
        {
          Destroy(order.person);
          Destroy(order.newOrderUI);
          Destroy(order.gameObject);
        }
      }
    }
  }

  public static void ResetOrdersNumber()
  {
    _orders_number = 0;
    Debug.Log("You have failed order");
  }

  private IEnumerator Cash_appear(int money)
  {
    float timeLeft = 2f;
    CashBoxText.text = "+" + money + "$";
    while (timeLeft > 0)
    {
      timeLeft -= Time.deltaTime;
      yield return null;
    }
    CashBoxText.text = "";
  }
}