using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using static GlobalVariables;
using static GlobalVariables.achievements;
using System.Collections.Generic;

public class Order : MonoBehaviour, Observable
{
    public BurgerRecipe burgerRecipe;
    public DrinkRecipe drinkRecipe;

    public GameObject orderUI, newOrderUI;

    public AudioClip sound;
    public float volume = 0.5f;

    public int hasBurger;
    public int hasDrink;

    public float orderTime;

    bool _lolipopWasGiven = false;
    public List<GameObject> observers_game_objects;
    static List<Observer> _observers; // static поле существует во всех объектах класса в согласованном виде (с одним и тем же значением)
    static int _lolipop_num = 0;

    void Start()
    {
        _observers = new List<Observer>();
        // здесь следует получить объект со сцены, на котором будет висеть скрипт AchievementObserver
        foreach (var game_obj in observers_game_objects)
        {
            AddObserver(game_obj.GetComponent<AchievementObserver>());
        }
    }
    public void GenerateOrder(GameObject ordersList, GameObject ordersListUI)
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => { Drop(); });
        eventTrigger.triggers.Add(pointerDown);

        hasBurger = Random.Range(0, 2);
        hasDrink = Random.Range(0, 2);
        if (hasBurger == 0 && hasDrink == 0)
        {
            hasBurger = 1;
        }
        orderTime = Times["Base"];

        newOrderUI = Instantiate(orderUI);
        newOrderUI.transform.SetParent(ordersListUI.transform.GetChild(ordersListUI.transform.childCount - 1), false);
        newOrderUI.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        newOrderUI.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        newOrderUI.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        newOrderUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        newOrderUI.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        newOrderUI.SetActive(false);

        if (hasBurger == 1)
        {
            burgerRecipe = BurgerRecipes[Random.Range(0, BurgerRecipes.Count)];
            orderTime += Times["roastTime"];
            newOrderUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Translate[burgerRecipe.name];
            newOrderUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
            gameObject.transform.GetChild(2).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Translate[burgerRecipe.name];
            gameObject.transform.GetChild(2).transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.black;

        }
        if (hasDrink == 1)
        {
            drinkRecipe = DrinkRecipes[Random.Range(0, DrinkRecipes.Count)];
            orderTime += Times["Drink"];
            newOrderUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Translate[drinkRecipe.name];
            newOrderUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.black;
            gameObject.transform.GetChild(2).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = Translate[drinkRecipe.name];
            gameObject.transform.GetChild(2).transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        ordersList.GetComponent<OrdersList>().PlaceOrder(orderTime, ordersListUI, transform, newOrderUI.transform);

        gameObject.GetComponent<AudioSource>().PlayOneShot(sound, volume);

        newOrderUI.SetActive(true);
        newOrderUI.GetComponent<OrderUI>().StartProgressBar(orderTime, gameObject);
    }

    private void Drop()
    {
        if (Hand.HasChildren())
        {
            if (_lolipopWasGiven == false && Hand.GetChildTag() == "Lolipop")
            {
                Debug.Log(newOrderUI.GetComponent<OrderUI>().timeToFill);
                newOrderUI.GetComponent<OrderUI>().startTime += 4f;
                _lolipopWasGiven = true;
                Destroy(Hand.GetTransform().GetChild(0).gameObject);

                _lolipop_num++;
                if (_lolipop_num == 10) NotifyObserver(lolipopAchiev);
            }
        }
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
