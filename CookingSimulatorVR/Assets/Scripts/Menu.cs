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
}
