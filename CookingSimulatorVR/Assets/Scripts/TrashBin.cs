using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GlobalVariables;
using static GlobalVariables.achievements;

public class TrashBin : MyInteractionManager
{
  public TextMeshProUGUI ScoreText;
  public AudioClip sound;
  public float volume = 1;

  [Range(0, 1)] public float value;
  List<Vector3> line1;
  Transform object1;

  int _utilized_objects_count = 0;

  public TextMeshProUGUI minus_money;
  int minus;
  protected override void Start()
  {
    base.Start();
    line1 = new List<Vector3>(2);
    line1.Add(new Vector3());
    line1.Add(transform.position + new Vector3(0.3f, 0.3f, 0f));
    line1.Add(transform.position);
    line1.Add(transform.position + new Vector3(0, -0.5f, -0.2f));
  }

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        DropIn(leftOculusHand);
      }
      else if (rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        DropIn(rightOculusHand);
      }
    }
  }

  void LerpLine()
  {
    List<Vector3> list = new List<Vector3>();
    for (int i = 0; i < line1.Count - 1; i++)
    {
      list.Add(Vector3.Lerp(line1[i], line1[i + 1], value));
    }
    Lerp2Line(list);
  }

  IEnumerator PlusValue()
  {
    while (value <= 1)
    {
      yield return new WaitForSeconds(0.01f);
      value += 0.03f;
      Move();
    }
    value = 0;
    Destroy(object1.gameObject);
  }
  void Move()
  {
    LerpLine();
  }
  void Lerp2Line(List<Vector3> list2)
  {
    if (list2.Count > 2)
    {
      List<Vector3> list = new List<Vector3>();
      for (int i = 0; i < list2.Count - 1; i++)
      {
        list.Add(Vector3.Lerp(list2[i], list2[i + 1], value));
      }
      Lerp2Line(list);
    }
    else
    {
      object1.position = Vector3.Lerp(list2[0], list2[1], value);
    }
  }

  void DropIn(OculusHand hand)
  {
    if (hand.HasChildren())
    {
      if (hand.GetChildTag() != "Untagged")
      {
        if (Costs.ContainsKey(hand.GetChildTag()))
        {
          line1[0] = hand.GetTransform().position;
          int childCount = hand.GetChild().childCount;
          minus = 0;
          if (childCount != 0)
          {
            for (int i = 0; i < childCount; i++)
            {
              scoreValue -= Costs[hand.GetChild().GetChild(i).tag];
              minus += Costs[hand.GetChild().GetChild(i).tag];
            }
          }
          scoreValue -= Costs[hand.GetChildTag()];
          minus += Costs[hand.GetChildTag()];
          StartCoroutine(Cash_appear(minus));
          ScoreText.text = scoreValue + "$";
          line1[0] = hand.GetTransform().position;
          object1 = hand.GetChild();
          StartCoroutine(PlusValue());
          gameObject.GetComponent<AudioSource>().PlayOneShot(sound, volume);

          _utilized_objects_count++;
          if (_utilized_objects_count == 10) AchievementManager.GetInstance().HandleEvent(trashBinAchiev);
        }
        else if (hand.GetChildTag() != "Order")
        {
          Debug.LogError("Unknown tag of object in hand");
        }
      }
      else
      {
        Debug.LogError("Add tag to object in hand");
      }
    }
  }

  private IEnumerator Cash_appear(int minus)
  {
    float timeLeft = 2f;
    minus_money.text = "-" + minus + "$";
    while (timeLeft > 0)
    {
      timeLeft -= Time.deltaTime;
      yield return null;
    }
    minus_money.text = "";
  }
}
