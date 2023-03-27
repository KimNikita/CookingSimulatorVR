using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class Menu : MonoBehaviour
{
  void Start()
  {
    if (!PlayerPrefs.HasKey("BestScore"))
    {
      PlayerPrefs.SetInt("BestScore", GlobalVariables.scoreValue);
    }
    if (!PlayerPrefs.HasKey("Difficulty"))
    {
      PlayerPrefs.SetString("Difficulty", "Easy");
    }

    transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Лучший результат: " + PlayerPrefs.GetInt("BestScore") + "$";
    transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Сложность: " + GlobalVariables.Translate[PlayerPrefs.GetString("Difficulty")];

    EventTrigger startTrigger = transform.GetChild(0).GetChild(0).gameObject.AddComponent<EventTrigger>();

    EventTrigger.Entry startPointerDown = new EventTrigger.Entry();
    startPointerDown.eventID = EventTriggerType.PointerDown;
    startPointerDown.callback.AddListener((eventData) => { StartGame(); });

    startTrigger.triggers.Add(startPointerDown);

    EventTrigger howtoTrigger = transform.GetChild(0).GetChild(1).gameObject.AddComponent<EventTrigger>();

    EventTrigger.Entry howtoPointerDown = new EventTrigger.Entry();
    howtoPointerDown.eventID = EventTriggerType.PointerDown;
    howtoPointerDown.callback.AddListener((eventData) => { HowTo(); });

    howtoTrigger.triggers.Add(howtoPointerDown);

    EventTrigger difficultyTrigger = transform.GetChild(0).GetChild(2).gameObject.AddComponent<EventTrigger>();

    EventTrigger.Entry difficultyPointerDown = new EventTrigger.Entry();
    difficultyPointerDown.eventID = EventTriggerType.PointerDown;
    difficultyPointerDown.callback.AddListener((eventData) => { Difficulty(); });

    difficultyTrigger.triggers.Add(difficultyPointerDown);
  }

  public void StartGame()
  {
    SceneManager.LoadScene(1);
  }

  public void HowTo()
  {
    Debug.Log("Not implemented");
  }

  public void Difficulty()
  {
    switch (PlayerPrefs.GetString("Difficulty"))
    {
      case "Easy": PlayerPrefs.SetString("Difficulty", "Medium"); break;
      case "Medium": PlayerPrefs.SetString("Difficulty", "Hard"); break;
      case "Hard": PlayerPrefs.SetString("Difficulty", "Easy"); break;
      default: Debug.Log("Unknown difficulty " + PlayerPrefs.GetString("Difficulty")); break;
    }
    transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Сложность: " + GlobalVariables.Translate[PlayerPrefs.GetString("Difficulty")];
  }
}
