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
  [SerializeField] List<GameObject> personsList;
  public GameObject personsPlacesList;

  void Start()
  {
    GetComponent<AudioSource>().Play();
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
      yield return new WaitForSeconds(Times["BetweenOrders"]); ;

      int i = 0;
      for (; i < personsPlacesList.transform.childCount; i++)
      {
        if (personsPlacesList.transform.GetChild(i).childCount == 0)
        {
          break;
        }
      }
      GameObject person = Instantiate(personsList[Random.Range(0, personsList.Count)], personsPlacesList.transform.GetChild(i).position, new Quaternion(0, 180, 0, 0), personsPlacesList.transform.GetChild(i));

      // генерируем заказ по шаблону, добавляя его в список на сцене и инициализируя
      GameObject newOrder = Instantiate(orderTemplate);
      newOrder.GetComponent<Order>().GenerateOrder(ordersList, ordersListUI, person);
    }

    yield return null;
  }
}
