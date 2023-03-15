using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
  public UnityEvent onBarFilled;

  private float timeToFill = 4.0f;

  private Image progressBarImage = null;
  public Coroutine barFillCoroutine = null;

  void Start()
  {
    progressBarImage = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
  }

  public void StartFillingProgressBar()
  {
    barFillCoroutine = StartCoroutine("Fill");
  }

  public void StopFillingProgressBar()
  {
    StopCoroutine(barFillCoroutine);
    progressBarImage.fillAmount = 0.0f;
  }

  IEnumerator Fill()
  {
    float startTime = Time.time;
    float overTime = startTime + timeToFill;

    while (Time.time < overTime)
    {
      progressBarImage.fillAmount = Mathf.Lerp(0, 1, (Time.time - startTime) / timeToFill);
      yield return null;
    }

    progressBarImage.fillAmount = 0.0f;
    GlobalVariables.end = true;

    if (onBarFilled != null)
    {
      onBarFilled.Invoke();
    }
    gameObject.AddComponent<Rigidbody>();
    gameObject.GetComponent<Rigidbody>().mass = 0.05f;
    Destroy(gameObject.GetComponent<EventTrigger>());
  }
}