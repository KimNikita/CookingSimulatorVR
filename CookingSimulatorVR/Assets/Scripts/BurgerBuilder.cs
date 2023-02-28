using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BurgerBuilder : MonoBehaviour
{
    List<Transform> ingredients;
    private void OnTriggerEnter(Collider other)
    {
        ingredients.Add(other.transform);
    }
    void Start()
    {
        ingredients = new List<Transform>();        
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => { OnPointerDown(); });
        eventTrigger.triggers.Add(pointerDown);
    }
    void OnPointerDown() 
    { 
        if(!Hand.HasChildren()) // if hand is empty - put the ingredient into it
        {
            MoveIngredientToHand();
        }
        else
        {
            MoveIngredientFromHand();
        }
    }
    void MoveIngredientToHand()
    {
        Transform last_ingr = ingredients.Last().transform;
        last_ingr.GetComponent<TakeIngredient>().MoveToHand();
        ingredients.RemoveAt(ingredients.Count - 1);
        Destroy(last_ingr);
    }
    void MoveIngredientFromHand()
    {
        if (ingredients.Count != 0)
        {
            Transform new_ingredient = GameObject.FindGameObjectWithTag("Hand").transform.GetChild(0);
            new_ingredient.parent = gameObject.transform;

            BoxCollider last_ingr_collider = ingredients.Last().transform.GetComponent<BoxCollider>();
            float last_ingr_y = last_ingr_collider.size.y;
            
            BoxCollider new_ingr_collider = new_ingredient.GetComponent<BoxCollider>();
            float new_ingr_y = new_ingr_collider.size.y;
            
            new_ingredient.transform.position = ingredients.Last().transform.position + new Vector3 (0, (last_ingr_y + new_ingr_y) / 2, 0);
            Debug.Log(ingredients.Last().transform.position.ToString("F4"));
            Debug.Log(new Vector3(0, (last_ingr_y + new_ingr_y) / 2, 0).ToString("F4"));
            Debug.Log((ingredients.Last().transform.position + new Vector3(0, (last_ingr_y + new_ingr_y) / 2, 0)).ToString("F4"));
            Debug.Log(new_ingredient.transform.position.ToString("F4"));

            ingredients.Add(new_ingredient);
        }
    }
}
