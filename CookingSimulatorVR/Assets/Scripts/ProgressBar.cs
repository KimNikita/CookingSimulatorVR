using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{
  public UnityEvent onBarFilled;
  public float timeToFill = 1.0f;
  public Coroutine barFillCoroutine = null;

  private Image progressBarImage = null;
  private void Start()
  {
    progressBarImage = GetComponent<Image>();

    if (progressBarImage == null)
    {
      Debug.LogError("Компонент Image отсутствует");
    }

    EventTrigger eventTrigger = gameObject.transform.parent.gameObject.AddComponent<EventTrigger>();

    //Ламбда-выражения имеют следующий синтаксис: слева от лямбда-оператора => определяется список параметров,
    //а справа блок выражений, использующий эти параметры
    EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
    pointerEnter.eventID = EventTriggerType.PointerEnter;
    pointerEnter.callback.AddListener((eventData) => { StartFillingProgressBar(); });
    eventTrigger.triggers.Add(pointerEnter);

    EventTrigger.Entry pointerExit = new EventTrigger.Entry();
    pointerExit.eventID = EventTriggerType.PointerExit;
    pointerExit.callback.AddListener((eventData) => { StopFillingProgressBar(); });
    eventTrigger.triggers.Add(pointerExit);
  }

  void StartFillingProgressBar()
  {
    barFillCoroutine = StartCoroutine("Fill");
  }

  void StopFillingProgressBar()
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

    if (onBarFilled != null)
    {
      onBarFilled.Invoke();
    }
  }
}
