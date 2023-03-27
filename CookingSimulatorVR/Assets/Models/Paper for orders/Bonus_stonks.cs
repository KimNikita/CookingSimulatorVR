using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Bonus_stonks : MyInteractionManager
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
  }

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        Double_money();
      }
    }
  }

  void Double_money()
  {
    if (flag)
    {
      time = 10f;
      timeLeft = 10f;
      flag = false;
      GlobalVariables.Costs["Tips"] = 20;
      interactiveObject.transform.position = new Vector3(2.302f, 2.5f, 2.15f);
      interactiveObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
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

    if (!flag) { GlobalVariables.Costs["Tips"] = 0; }
    Destroy(gameObject);
  }
}
