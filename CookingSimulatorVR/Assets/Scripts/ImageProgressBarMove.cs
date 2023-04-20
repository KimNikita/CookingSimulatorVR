using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageProgressBarMove : MonoBehaviour
{
  public UnityEvent onBarFilled;

  [Header("Custom Settiings")]
  public bool alwaysFacePlayer = true;
  public bool disableOnFill = false;

  // Время в секундах необходимое для заполнения Progressbar'а
  public float timeToFill = 1.0f;

  // Переменная, куда будет сохранена ссылка на компонент Image
  // текущего объекта, который является ProgressBar'ом
  private Image progressBarImage = null;
  public Coroutine barFillCoroutine = null;

  void Start()
  {
    // Получаем ссылку на компонент Image текущего объекта при
    // помощи метода GetComponent<>();
    progressBarImage = GetComponent<Image>();

    // Если у данного объекта нет компонента Image выводим ошибку
    // в консоль
    if (progressBarImage == null)
    {
      Debug.LogError("There is no referenced image on this component!");
    }

    // EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

    // Создаём контроллер для события наведения указателя на объект
    EventTrigger eventTrigger = gameObject.transform.parent.gameObject.AddComponent<EventTrigger>();

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

    if (disableOnFill)
    {
      transform.parent.gameObject.SetActive(false);
    }
  }
}