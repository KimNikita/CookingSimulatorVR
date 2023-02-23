using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrdersList : MonoBehaviour
{
  // содержит префабы OrderPlace в качестве детей

  // Вариант где новый заказ всегда появляется правее остальных
  /*
  public Transform GetNewOrderPlace()
  {
    // двигаем все заказы влево, если самое правое место под заказ не свободно
    if (gameObject.transform.GetChild(0).transform.childCount != 0)
    {
      for (int i = gameObject.transform.childCount - 1; i > 0; i--)
      {
        // если правее висит заказ, то двигаем его влево
        if (gameObject.transform.GetChild(i - 1).childCount != 0)
        {
          gameObject.transform.GetChild(i - 1).GetChild(0).position = new Vector3(gameObject.transform.GetChild(i).position.x, gameObject.transform.GetChild(i).position.y - 0.05f, gameObject.transform.GetChild(i).position.z);
          gameObject.transform.GetChild(i - 1).GetChild(0).parent = gameObject.transform.GetChild(i);
        }
      }
    }

    return gameObject.transform.GetChild(0).transform;
  }
  */

  // Вариант где новый заказ всегда появляется левее остальных
  public Transform GetNewOrderPlace()
  {
    for (int i = gameObject.transform.childCount - 1; i > 0; i--)
    {
      if (gameObject.transform.GetChild(i - 1).childCount != 0)
      {
        return gameObject.transform.GetChild(i).transform;
      }
    }
    return gameObject.transform.GetChild(0).transform;
  }

}