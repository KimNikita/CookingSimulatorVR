using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BurgerBuilder : MonoBehaviour
{
  private Transform tray;

  void Start()
  {
    tray = gameObject.transform;
    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { OnPointerDown(); });
    eventTrigger.triggers.Add(pointerDown);
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
      if (tray.childCount == 0 && ingredientTag == "Bun")
      {
        Transform burger = Hand.GetTransform().GetChild(0);
        burger.transform.position = tray.transform.position + new Vector3(0.035f, 0, 0.05f); // костыль
        burger.transform.rotation = tray.transform.rotation;
        burger.parent = tray.transform;
      }
      else if (tray.childCount != 0)
      {
        if (ingredientTag == "Cheese" || ingredientTag == "Tomato" || ingredientTag == "Cooked Beef" || ingredientTag == "Bun")
        {
          Transform new_ingredient = Hand.GetTransform().GetChild(0);

          BoxCollider last_ingr_collider;
          if (tray.GetChild(0).childCount == 0)
          {
            last_ingr_collider = tray.GetChild(0).GetComponent<BoxCollider>();
          }
          else
          {
            last_ingr_collider = tray.GetChild(0).GetChild(tray.GetChild(0).childCount - 1).GetComponent<BoxCollider>();
          }

          float last_ingr_y = last_ingr_collider.size.y;

          BoxCollider new_ingr_collider = new_ingredient.GetComponent<BoxCollider>();
          float new_ingr_y = new_ingr_collider.size.y;

          new_ingredient.transform.position = last_ingr_collider.gameObject.transform.position + new Vector3(0, last_ingr_y / 2 + new_ingr_y / 1.9f, 0);
          new_ingredient.transform.rotation = last_ingr_collider.gameObject.transform.rotation;
          new_ingredient.parent = tray.GetChild(0).transform;
        }
      }
    }
  }
}
