using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GlobalVariables;

public class Bonus_stonks : MonoBehaviour
{

  public GameObject interactiveObject;
  [SerializeField] private float time;
  [SerializeField] public Image timerImage;
  private float timeLeft = 0f;

  public AudioClip sound;
  public float volume = 0.5f;

  private bool flag = true;

  void Start()
  {
    timeLeft = time;
    interactiveObject.GetComponent<AudioSource>().PlayOneShot(sound, volume);
    StartCoroutine(StartTimer());

    EventTrigger eventTrigger = interactiveObject.AddComponent<EventTrigger>();
    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { Double_money(); });
    eventTrigger.triggers.Add(pointerDown);
  }

  void Double_money()
  {
    if (flag)
    {
      time = 10f;
      timeLeft = 10f;
      flag = false;
      Costs["Tips"] = 20;
      interactiveObject.transform.position = new Vector3(2.302f, 2.5f, 2.15f);
      interactiveObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
  }

  private IEnumerator StartTimer()
  {
    while (timeLeft > 0)
    {
      timeLeft -= Time.deltaTime;
      var normalizedValue = Mathf.Clamp(timeLeft / time, 0.0f, 1.0f);
      timerImage.fillAmount = normalizedValue;
      yield return null;
    }

    if (!flag) { Costs["Tips"] = 0; }
    Destroy(gameObject);
  }
}
