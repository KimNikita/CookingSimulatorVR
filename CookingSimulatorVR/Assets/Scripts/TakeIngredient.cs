using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalVariables;

public class TakeIngredient : MyInteractionManager
{
  public GameObject ingredientPrefab;
  public string ingredientTag;

  [Range(0, 1)] public float value;
  List<Vector3> _line;
  Transform _object_to_move;
  bool canTakeIngredient = true; // canTakeIngredient ������� false, ���� ���������� �� "�������" �� ����� ����������

  protected override void Start()
  {
    base.Start();
    _line = new List<Vector3>(2);
    _line.Add(new Vector3());
    _line.Add(new Vector3());
  }

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        MoveToHand(leftOculusHand);
      }
      else if (rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        MoveToHand(rightOculusHand);
      }
    }
  }

  void LerpLine()
  {
    _object_to_move.position = Vector3.Lerp(_line[0], _line[1], value);
  }
  void Move()
  {
    LerpLine();
  }
  IEnumerator MinusValue(OculusHand hand)
  {
    canTakeIngredient = false;
    value = 1;
    while (value >= 0)
    {
      yield return new WaitForSeconds(0.01f);
      _line[0] = hand.GetPosition() + Offsets[_object_to_move.tag];
      value -= 0.07f;
      Move();
    }
    canTakeIngredient = true;
    _object_to_move.transform.rotation = new Quaternion(0, hand.GetRotation().y, 0, hand.GetRotation().w);
    _object_to_move.transform.parent = hand.GetTransform();
  }

  public void MoveToHand(OculusHand hand)
  {
    if (!hand.HasChildren())
    {
      _line[0] = hand.GetPosition();
      GameObject instance = Instantiate(ingredientPrefab);
      _object_to_move = instance.transform;
      _line[1] = _object_to_move.tag == "Lolipop" ? instance.transform.position : gameObject.transform.position; // берётся позиция Spawner, т.к. у префаба позиция неподходящая
      instance.tag = ingredientTag;
      instance.AddComponent<BoxCollider>();
      StartCoroutine(MinusValue(hand));
    }
  }
}
