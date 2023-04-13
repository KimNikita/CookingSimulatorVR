using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static GlobalVariables;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
  public UnityEvent onBarFilled;
  public bool enable;

  private float timeToFill = 4.0f;

  private Image progressBarImage = null;
  public Coroutine barFillCoroutine = null;

  public TextMeshProUGUI Score;
  public TextMeshProUGUI BestScore;

  void Start()
  {
    enable = true;
    progressBarImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();

  }

  public void StartFillingProgressBar()
  {
    if (enable)
    {
      barFillCoroutine = StartCoroutine("Fill");
    }
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

    if (SceneManager.GetActiveScene().name == "MainScene")
    {
      end = true;
      Score.text = "     Ваш результат: " + scoreValue + "$";
      if (scoreValue > PlayerPrefs.GetInt("BestScore"))
      {
        PlayerPrefs.SetInt("BestScore", scoreValue);
      }
      BestScore.text = "Лучший результат: " + PlayerPrefs.GetInt("BestScore") + "$";

      if (onBarFilled != null)
      {
        enable = false;
        onBarFilled.Invoke();
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().mass = 0.05f;
      }
    }
    else if (SceneManager.GetActiveScene().name == "MainMenu")
    {
      Application.Quit();
    }

  }
}