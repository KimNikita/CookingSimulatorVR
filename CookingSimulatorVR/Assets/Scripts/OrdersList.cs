using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrdersList : MonoBehaviour
{

  // Вариант где новый заказ сортируется вставкой
  public void PlaceOrder(float orderTime, GameObject ordersListUI, Transform order, Transform orderUI)
  {
    int uiCount = 0;
    for (int i = transform.childCount - 1; i >= 0; i--)
    {
      if (transform.GetChild(i).childCount > 1)
      {
        uiCount++;
        OrderUI tempOrderUI = transform.GetChild(i).GetChild(1).GetComponent<Order>().newOrderUI.GetComponent<OrderUI>();
        if (tempOrderUI.timeToFill * tempOrderUI.progressBarImage.fillAmount > orderTime)
        {
          for (int j = transform.childCount - 1; j > i + 1; j--)
          {
            if (transform.GetChild(j - 1).childCount > 1)
            {
              transform.GetChild(j - 1).GetChild(1).position = new Vector3(transform.GetChild(j).position.x, transform.GetChild(j).position.y - 0.05f, transform.GetChild(j).position.z);
              transform.GetChild(j - 1).GetChild(1).parent = transform.GetChild(j);
            }
          }

          int k = ordersListUI.transform.childCount - 1;
          while (ordersListUI.transform.GetChild(k).childCount == 0)
          {
            k--;
          }
          int uiIndex = k - uiCount + 1;

          for (int j = k + 1; j > uiIndex + 1; j--)
          {
            //ordersListUI.transform.GetChild(j - 1).GetChild(0).position = ordersListUI.transform.GetChild(j).position;
            ordersListUI.transform.GetChild(j - 1).GetChild(0).SetParent(ordersListUI.transform.GetChild(j));
          }

          order.position = new Vector3(transform.GetChild(i + 1).position.x, transform.GetChild(i + 1).position.y - 0.05f, transform.GetChild(i + 1).position.z);
          order.rotation = transform.GetChild(i + 1).rotation;
          order.parent = transform.GetChild(i + 1);

          //orderUI.position = ordersListUI.transform.GetChild(uiIndex + 1).position;
          orderUI.SetParent(ordersListUI.transform.GetChild(uiIndex + 1));

          return;
        }
      }
    }

    int index = 0;
    if (transform.GetChild(0).childCount > 1)
    {
      OrderUI tempOrderUI2 = transform.GetChild(0).GetChild(1).GetComponent<Order>().newOrderUI.GetComponent<OrderUI>();
      if (tempOrderUI2.timeToFill * tempOrderUI2.progressBarImage.fillAmount > orderTime)
      {
        index = 1;
      }

      for (int j = transform.childCount - 1; j > index; j--)
      {
        if (transform.GetChild(j - 1).childCount > 1)
        {
          transform.GetChild(j - 1).GetChild(1).position = new Vector3(transform.GetChild(j).position.x, transform.GetChild(j).position.y - 0.05f, transform.GetChild(j).position.z);
          transform.GetChild(j - 1).GetChild(1).parent = transform.GetChild(j);
        }
      }

      for (int j = ordersListUI.transform.childCount - 1; j > index; j--)
      {
        if (ordersListUI.transform.GetChild(j - 1).childCount != 0)
        {
          //ordersListUI.transform.GetChild(j - 1).GetChild(0).position = ordersListUI.transform.GetChild(j).position;
          ordersListUI.transform.GetChild(j - 1).GetChild(0).SetParent(ordersListUI.transform.GetChild(j));
        }
      }
    }

    order.position = new Vector3(transform.GetChild(index).position.x, transform.GetChild(index).position.y - 0.05f, transform.GetChild(index).position.z);
    order.rotation = transform.GetChild(index).rotation;
    order.parent = transform.GetChild(index);

    //orderUI.position = ordersListUI.transform.GetChild(index).position;
    orderUI.SetParent(ordersListUI.transform.GetChild(index));
  }


  // Вариант где новый заказ всегда появляется левее остальных bad variant
  /*
  public Transform GetNewOrderPlace()
  {
    for (int i = gameObject.transform.childCount - 1; i > 0; i--)
    {
      if (gameObject.transform.GetChild(i - 1).childCount == 2)
      {
        return gameObject.transform.GetChild(i).transform;
      }
    }
    return gameObject.transform.GetChild(0).transform;
  }
  */
}