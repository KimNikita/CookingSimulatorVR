using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TrashBin : MyInteractionManager
{
  public TextMeshProUGUI ScoreText;
  public AudioClip sound;
  public float volume = 1;

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

  void DropIn(OculusHand hand)
  {
    if (hand.HasChildren())
    {
      if (hand.GetChildTag() != "Untagged")
      {
        if (GlobalVariables.Costs.ContainsKey(hand.GetChildTag()))
        {
          int childCount = hand.GetChild().childCount;
          if (childCount != 0)
          {
            for (int i = 0; i < childCount; i++)
            {
              GlobalVariables.scoreValue -= GlobalVariables.Costs[hand.GetChild().GetChild(i).tag];
            }
          }
          GlobalVariables.scoreValue -= GlobalVariables.Costs[hand.GetChildTag()];
          ScoreText.text = GlobalVariables.scoreValue + "$";
          Destroy(hand.GetChild().gameObject);
          gameObject.GetComponent<AudioSource>().PlayOneShot(sound, volume);
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
    else
    {
      Debug.Log("wrong check children");
    }
  }
}