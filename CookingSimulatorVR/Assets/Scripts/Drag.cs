using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    [Range(0, 1)] public float value;
    List<Vector3> _line;
    Transform _object_to_move;
    bool canTakeIngredient = true; // canTakeIngredient сетится false, пока ингредиент не "долетел" до места назначения
    void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((eventData) => { MoveToHand(); });
        eventTrigger.triggers.Add(pointerDown);

        _line = new List<Vector3>(2);
        _line.Add(new Vector3());
        _line.Add(new Vector3());
    }
    void LerpLine()
    {
        _object_to_move.position = Vector3.Lerp(_line[0], _line[1], value);
    }
    void Move()
    {
        LerpLine();
    }
    IEnumerator PlusValue()
    {
        canTakeIngredient = false;
        while (value <= 1)
        {
            yield return new WaitForSeconds(0.01f);
            value += 0.07f;
            Move();
        }
        canTakeIngredient = true;
        gameObject.transform.rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
        gameObject.transform.parent = Hand.GetTransform();
        value = 0;
    }
    void MoveToHand()
    {
        _line[0] = gameObject.transform.position;
        if (!Hand.HasChildren())
        {
            _object_to_move = gameObject.transform;
            if (gameObject.tag == "Fanta" || gameObject.tag == "Cola")
            {
                _line[1] = new Vector3(Hand.GetPosition().x, Hand.GetPosition().y - 0.25f, Hand.GetPosition().z);
                GlobalVariables.hasDrink = false;
            }
            else
            {
                _line[1] = Hand.GetPosition();
            }
            StartCoroutine(PlusValue());
        }
    }
}
