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
