using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GlobalVariables;

public class GameManager : MonoBehaviour
{
  public TextMeshProUGUI ScoreText;
  public GameObject orderTemplate;
  public GameObject ordersList;
  public GameObject ordersListUI;

  void Start()
  {
    gameObject.GetComponent<AudioSource>().Play();
    ScoreText.text = scoreValue + "$";
    switch (PlayerPrefs.GetString("Difficulty"))
    {
      case "Easy": Times["BetweenOrders"] = 25; Costs["NDS"] = 5; break;
      case "Medium": Times["BetweenOrders"] = 20; Costs["NDS"] = 15; break;
      case "Hard": Times["BetweenOrders"] = 15; Costs["NDS"] = 25; break;
      default: Debug.Log("Unknown difficulty " + PlayerPrefs.GetString("Difficulty")); break;
    }
    StartCoroutine(OrderSpawner());
  }

  IEnumerator OrderSpawner()
  {
    while (!end)
    {
      // ждем перед генерацией нового заказа
      yield return new WaitForSeconds(Times["BetweenOrders"]);

      // генерируем заказ по шаблону, добавляя его в список на сцене и инициализируя
      GameObject newOrder = Instantiate(orderTemplate);
      newOrder.GetComponent<Order>().GenerateOrder(ordersList, ordersListUI);
    }
    // save results

    yield return null;
  }
}
