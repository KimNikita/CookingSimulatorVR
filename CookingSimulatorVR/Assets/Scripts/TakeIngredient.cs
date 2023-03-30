using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TakeIngredient : MonoBehaviour
{
    public GameObject ingredientPrefab;
    public string ingredientTag;

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
    IEnumerator MinusValue()
    {
        canTakeIngredient = false;
        value = 1;
        while (value >= 0)
        {
            yield return new WaitForSeconds(0.01f);
            _line[0] = Hand.GetPosition();
            value -= 0.07f;
            Move();
        }
        canTakeIngredient = true;
        _object_to_move.transform.rotation = new Quaternion(0, Hand.GetRotation().y, 0, Hand.GetRotation().w);
        _object_to_move.transform.parent = Hand.GetTransform();
    }

    public void MoveToHand()
    {        
        if (!Hand.HasChildren())
        {
            _line[0] = Hand.GetPosition();
            GameObject instance = Instantiate(ingredientPrefab);
            _object_to_move = instance.transform;
            _line[1] = _object_to_move.tag == "Lolipop" ? instance.transform.position: gameObject.transform.position; // берётся позиция Spawner, т.к. у префаба позиция неподходящая
            instance.tag = ingredientTag;
            instance.AddComponent<BoxCollider>();            
            StartCoroutine(MinusValue());
        }
    }
}
