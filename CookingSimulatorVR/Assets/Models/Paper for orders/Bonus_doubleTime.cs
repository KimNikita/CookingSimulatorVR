﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;


public class Bonus_doubleTime : MyInteractionManager
{
  public GameObject interactiveObject;
  [SerializeField] private float time;
  [SerializeField] public Image timerImage;
  private float timeLeft = 0f;


  public AudioClip sound;
  public float volume = 0.5f;

  private bool  notActive = true;

  void Start()
  {
    timeLeft = time;
    interactiveObject.GetComponent<AudioSource>().PlayOneShot(sound, volume);
    StartCoroutine(StartTimer());
  }

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        Double_time();
      }
    }
  }

  void Double_time()
  {
    if (notActive)
    {
      time = 10f;
      timeLeft = 10f;
      notActive = false;
      GlobalVariables.Times["roastTime"] /= 2;
      interactiveObject.transform.position = new Vector3(2.302f, 3f, 2.15f);
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

    if (!notActive) { GlobalVariables.Times["roastTime"] *= 2; }
    Destroy(gameObject);
  }
}
