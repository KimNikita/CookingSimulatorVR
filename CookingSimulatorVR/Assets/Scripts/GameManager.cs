using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
  public TextMeshProUGUI ScoreText;
  public GameObject orderTemplate;
  public GameObject ordersList;

  void Start()
  {
    ScoreText.text = GlobalVariables.scoreValue + "$";
    StartCoroutine(OrderSpawner());
  }

  IEnumerator OrderSpawner()
  {
    while (!GlobalVariables.end)
    {
      // ждем перед генерацией нового заказа
      yield return new WaitForSeconds(GlobalVariables.Times["BetweenOrders"]);

      // генерируем заказ по шаблону, добавляя его в список на сцене и инициализируя
      Transform newOrderPlace = ordersList.GetComponent<OrdersList>().GetNewOrderPlace();
      GameObject newOrder = Instantiate(orderTemplate, new Vector3(newOrderPlace.position.x, newOrderPlace.position.y - 0.05f, newOrderPlace.position.z), newOrderPlace.rotation, newOrderPlace);
      newOrder.GetComponent<Order>().GenerateOrder();
    }
    // save results

    yield return null;
  }
}
