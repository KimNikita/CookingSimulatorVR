using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BurgerBuilder : MonoBehaviour
{
  public List<Transform> ingredients;

  void Start()
  {
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
      Debug.Log("Move Ingr to Hand");
      MoveBunToHand();
    }
    else
    {
      Debug.Log("Move Ingr from Hand");
      MoveIngredientFromHand();
    }
  }
  void MoveBunToHand()
  {
    if (gameObject.transform.childCount != 0)
    {
      gameObject.transform.GetChild(0).rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
      gameObject.transform.GetChild(0).transform.position = Hand.GetPosition();
      gameObject.transform.GetChild(0).transform.parent = Hand.GetTransform();
    }
  }
  void MoveIngredientFromHand()
  {
    if (gameObject.transform.childCount != 0)
    {
      Debug.Log("working");
      // TODO fix rotation, fix colliders, fix spavn ingredients

      Transform new_ingredient = Hand.GetTransform().GetChild(0);

      BoxCollider last_ingr_collider;
      if (gameObject.transform.GetChild(0).childCount == 0)
      {
        last_ingr_collider = gameObject.transform.GetChild(0).GetComponent<BoxCollider>();
      }
      else
      {
        last_ingr_collider = gameObject.transform.GetChild(0).GetChild(gameObject.transform.GetChild(0).childCount - 1).GetComponent<BoxCollider>();
      }

      float last_ingr_y = last_ingr_collider.size.y;

      BoxCollider new_ingr_collider = new_ingredient.GetComponent<BoxCollider>();
      float new_ingr_y = new_ingr_collider.size.y;

      new_ingredient.transform.position = last_ingr_collider.gameObject.transform.position + new Vector3(0, (last_ingr_y + new_ingr_y)/2, 0);
      new_ingredient.parent = gameObject.transform.GetChild(0).transform;
    }
  }
}
