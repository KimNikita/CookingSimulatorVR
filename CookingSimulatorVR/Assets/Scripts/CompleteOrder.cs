using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CompleteOrder : MyInteractionManager
{
  public TextMeshProUGUI ScoreText;
  public GameObject tray;
  public AudioClip right;
  public AudioClip wrong;
  public float volume = 1;

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
        int money = GlobalVariables.Costs["Bun"];
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
                money += GlobalVariables.Costs[order.burgerRecipe.ingredients[i + 1]] + GlobalVariables.Costs["NDS"] + GlobalVariables.Costs["Tips"];
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
            money += GlobalVariables.Costs[order.drinkRecipe.name] + GlobalVariables.Costs["NDS"] + GlobalVariables.Costs["Tips"];
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

        GlobalVariables.scoreValue += money;
        ScoreText.text = GlobalVariables.scoreValue + "$";
        gameObject.GetComponent<AudioSource>().PlayOneShot(right, volume);
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
          Destroy(order.newOrderUI);
          Destroy(order.gameObject);
        }
      }
    }
  }
}