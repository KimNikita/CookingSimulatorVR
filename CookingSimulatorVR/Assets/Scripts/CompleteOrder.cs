﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using static GlobalVariables;
using static GlobalVariables.achievements;

public class CompleteOrder : MonoBehaviour, Observable
{
    public TextMeshProUGUI ScoreText;
    public GameObject tray;
    public AudioClip right;
    public AudioClip wrong;
    public float volume = 1;

    List<Observer> _observers;
    static int _orders_number = 0;

    void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => { Complete(); });

        eventTrigger.triggers.Add(pointerDown);
        
        _observers = new List<Observer>();
        // здесь следует получить объект со сцены, на котором будет висеть скрипт AchievementObserver
        Observer obs = new AchievementObserver();
        AddObserver(obs);
    }

    void Complete()
    {
        if (Hand.HasChildren())
        {
            if (Hand.GetChildTag() == "Order")
            {
                int money = Costs["Bun"];
                Order order = Hand.GetTransform().GetChild(0).GetComponent<Order>();
                if (order.hasBurger != 0)
                {
                    if (tray.transform.GetChild(2).childCount != 0 && tray.transform.GetChild(2).GetChild(0).childCount + 1 == order.burgerRecipe.ingredients.Count)
                    {
                        Transform burger = tray.transform.GetChild(2).GetChild(0);
                        for (int i = 0; i < burger.childCount; i++)
                        {
                            if (burger.GetChild(i).tag == order.burgerRecipe.ingredients[i + 1])
                            {
                                money += Costs[order.burgerRecipe.ingredients[i + 1]] + Costs["NDS"] + Costs["Tips"];
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

                _orders_number++;
                if (_orders_number == 10) NotifyObserver(orderAchiev);
                if ((scoreValue - money) < 1000 && scoreValue >= 1000) NotifyObserver(moneyAchiev);

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

    public static void ResetOrdersNumber() 
    { 
        _orders_number = 0;
        Debug.Log("You have failed order");
    }

    // методы интерфейса Observable, позволяют возбуждать события для AchivementObserver'а
    public void AddObserver(Observer o)
    {
        _observers.Add(o);
    }
    public void RemoveObserver(Observer o)
    {
        _observers.Remove(o);
    }

    public void NotifyObserver(achievements ach)
    {
        foreach (var ob in _observers)
        {
            ob.HandleEvent(ach);
        }
    }
}