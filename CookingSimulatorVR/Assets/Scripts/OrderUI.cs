using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
  public float timeToFill;
  public float startTime;

  private GameObject order;
  public Image progressBarImage = null;

  public Coroutine barFillCoroutine = null;

  public void StartProgressBar(float _timeToFill, GameObject _order)
  {
    timeToFill = _timeToFill;
    order = _order;
    barFillCoroutine = StartCoroutine("Fill");
  }

  IEnumerator Fill()
  {
    startTime = Time.time;

    while (progressBarImage.fillAmount>0)
    {
      progressBarImage.fillAmount = Mathf.Lerp(1, 0, (Time.time - startTime) / timeToFill);
      if (progressBarImage.fillAmount <= 0.5f && progressBarImage.fillAmount > 0.25)
      {
        progressBarImage.color = new Color(255, 127, 0, 255);
      }
      else if (progressBarImage.fillAmount <= 0.25f)
      {
        progressBarImage.color = new Color(255, 0, 0, 255);
      }
      else
      {
        progressBarImage.color = new Color(0, 255, 0, 255);
      }
      yield return null;
    }

    Destroy(order);
    Destroy(gameObject);
  }
}