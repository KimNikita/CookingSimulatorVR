using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Bonus_stonks : MonoBehaviour
{

  public GameObject interactiveObject;
  [SerializeField] private float time;
  [SerializeField] public Image timerImage;
  private float timeLeft = 0f;


  public AudioClip sound;
  public float volume = 0.5f;

  

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
    time = 10f;
    timeLeft = 10f;
    GlobalVariables.Costs["Tips"] = 20;
    interactiveObject.transform.parent = null;
    interactiveObject.transform.position = new Vector3(1.822f, 1.79f, -1.251f);
    interactiveObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

  }

  private IEnumerator StartTimer()
  {
    while(timeLeft > 0)
    {
      timeLeft -= Time.deltaTime;
      var normalizedValue = Mathf.Clamp(timeLeft / time, 0.0f, 1.0f);
      timerImage.fillAmount = normalizedValue;
      yield return null;
    }

    GlobalVariables.Costs["Tips"] = 0;
    Destroy(gameObject);
  }
}
