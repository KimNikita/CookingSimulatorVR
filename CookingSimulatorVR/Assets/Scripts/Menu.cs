using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class Menu : MonoBehaviour
{
  public GameObject achievPanel;
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

    transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Сложность: " + GlobalVariables.Translate[PlayerPrefs.GetString("Difficulty")];
  }
  private void Achievements()
  {
    Instantiate(achievPanel);
  }
  private void ResetRecord()
  {
    var ach_list = new string[]{ "cheeseAchiev", "lolipopAchiev", "moneyAchiev", "orderAchiev", "trashBinAchiev" };
    foreach(string achiev_key in ach_list)
    {
      PlayerPrefs.DeleteKey(achiev_key);
    }
    PlayerPrefs.SetInt("BestScore", 0);
    transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Лучший результат: 0$";
  }
}
