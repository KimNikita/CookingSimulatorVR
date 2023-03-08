using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BurgerBuilder : MonoBehaviour
{
    private Transform tray;
    [Range(0, 1)] public float value;
    List<Vector3> line1;
    Transform object1;
    void Start()
    {
        tray = gameObject.transform;
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => { OnPointerDown(); });
        eventTrigger.triggers.Add(pointerDown);

        line1 = new List<Vector3>(2);
        line1.Add(Hand.GetTransform().position); // точка из которой начинается движение               
    }
    void LerpLine1()
    {
        object1.position = Vector3.Lerp(line1[0], line1[1], value);
    }
    IEnumerator PlusValue()
    {
        object1.transform.rotation = tray.GetChild(0).rotation;
        object1.transform.rotation = new Quaternion(0, 90, 0, 0);
        while (value <= 1)
        {
            yield return new WaitForSeconds(0.01f);
            value += 0.01f;
            Move();
        }
        object1.parent = tray.GetChild(0).transform;
        value = 0;
    }
    void Move()
    {
        LerpLine1();
        DrawLine1();
    }
    void DrawLine1()
    {
        Debug.DrawLine(line1[0], line1[1], Color.red, 0.01f);
    }   
    Transform GetLastIngredient()
    {
        Transform lastIngr = null;
        if (tray.GetChild(0).childCount == 0)
        {
            lastIngr = tray.GetChild(0);
        }
        else
        {
            lastIngr = tray.GetChild(0).GetChild(tray.GetChild(0).childCount - 1);
        }
        return lastIngr;
    }
    void OnPointerDown()
    {
        if (!Hand.HasChildren()) // if hand is empty - put the ingredient into it
        {
            if (tray.childCount != 0)
            {
                tray.GetChild(0).rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
                tray.GetChild(0).transform.position = Hand.GetPosition();
                tray.GetChild(0).transform.parent = Hand.GetTransform();
            }
        }
        else
        {
            string ingredientTag = Hand.GetChildTag();
            // TODO may be need list of possible ingredients
            /*if (tray.childCount == 0 && ingredientTag == "Bun") // Алиса: у меня из-за этого кода всё ломалось. что это такое вообще?
            {
                Transform burger = Hand.GetTransform().GetChild(0);
                burger.transform.position = tray.transform.position + new Vector3(0.035f, 0, 0.05f); // костыль
                burger.transform.rotation = tray.transform.rotation;
                burger.parent = tray.transform;
            }
            else*/
            if (tray.childCount != 0)
            {
                if (ingredientTag == "Cheese" || ingredientTag == "Tomato" || ingredientTag == "Cooked Beef" || ingredientTag == "Bun")
                {
                    object1 = Hand.GetTransform().GetChild(0);
                    object1.parent = tray; // это не удалять. так ингредиенты ложатся хотя бы параллельно подносу

                    // расчёт точки приземления
                    BoxCollider last_ingr_collider = GetLastIngredient().GetComponent<BoxCollider>();
                    float last_ingr_y = last_ingr_collider.size.y;

                    BoxCollider new_ingr_collider = object1.GetComponent<BoxCollider>();
                    float new_ingr_y = new_ingr_collider.size.y;

                    Vector3 destination = last_ingr_collider.gameObject.transform.position + new Vector3(0, last_ingr_y / 2f + new_ingr_y / 2f, 0);
                    line1.Add(destination);

                    StartCoroutine(PlusValue());
                }
            }
        }
    }
}
