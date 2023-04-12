using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
  public UnityEvent onBarFilled;

  private float timeToFill = 4.0f;

  private Image progressBarImage = null;
  public Coroutine barFillCoroutine = null;

  public TextMeshProUGUI Score;
  public TextMeshProUGUI BestScore;

  void Start()
  {
    if (SceneManager.GetActiveScene().name == "MainScene")
    {
      progressBarImage = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }
    else if (SceneManager.GetActiveScene().name == "MainMenu")
    {
      progressBarImage = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

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
    
    if(SceneManager.GetActiveScene().name == "MainScene") {
      GlobalVariables.end = true;
      Score.text = "     Ваш результат: " + GlobalVariables.scoreValue + "$";
      if (GlobalVariables.scoreValue > PlayerPrefs.GetInt("BestScore"))
      {
        PlayerPrefs.SetInt("BestScore", GlobalVariables.scoreValue);
      }
      BestScore.text = "Лучший результат: " + PlayerPrefs.GetInt("BestScore") + "$";

      if (onBarFilled != null)
      {
        onBarFilled.Invoke();
      }
      gameObject.AddComponent<Rigidbody>();
      gameObject.GetComponent<Rigidbody>().mass = 0.05f;
      Destroy(gameObject.GetComponent<EventTrigger>());
    }
    else if (SceneManager.GetActiveScene().name == "MainMenu")
    {
      Debug.Log("Exit");
      Application.Quit();
    }
  }
}