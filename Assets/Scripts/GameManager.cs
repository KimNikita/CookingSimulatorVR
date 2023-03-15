using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public GameObject orderTemplate;
    public GameObject ordersList;
    public GameObject ordersListUI;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
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
            GameObject newOrder = Instantiate(orderTemplate);
            newOrder.GetComponent<Order>().GenerateOrder(ordersList, ordersListUI);
        }
        // save results

        yield return null;
    }
}
