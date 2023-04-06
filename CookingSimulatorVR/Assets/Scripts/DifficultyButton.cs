using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class DifficultyButton : MyInteractionManager
{
  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        Difficulty();
      }
    }
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
    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Сложность: " + GlobalVariables.Translate[PlayerPrefs.GetString("Difficulty")];
  }
}
