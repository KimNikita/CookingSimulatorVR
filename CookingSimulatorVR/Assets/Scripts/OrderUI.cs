using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
  public float timeToFill;

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
    float startTime = Time.time;
    float overTime = startTime + timeToFill;

    while (Time.time < overTime)
    {
      // TODO добавить изменение цвета
      progressBarImage.fillAmount = Mathf.Lerp(1, 0, (Time.time - startTime) / timeToFill);
      yield return null;
    }

    progressBarImage.fillAmount = 0.0f;
    Destroy(order);
    Destroy(gameObject);
  }
}