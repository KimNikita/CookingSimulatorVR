using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

    EventTrigger.Entry pointerDown = new EventTrigger.Entry();
    pointerDown.eventID = EventTriggerType.PointerDown;
    pointerDown.callback.AddListener((eventData) => { GoToMenu(); });

    eventTrigger.triggers.Add(pointerDown);
  }

  public void GoToMenu()
  {
    GlobalVariables.scoreValue = 100;
    GlobalVariables.end = false;
    SceneManager.LoadScene(0);
  }
}
