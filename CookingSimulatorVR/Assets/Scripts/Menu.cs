using UnityEngine;
using TMPro;
using static GlobalVariables;

public class Menu : MonoBehaviour
{
  void Start()
  {
    if (!PlayerPrefs.HasKey("BestScore"))
    {
      PlayerPrefs.SetInt("BestScore", scoreValue);
    }
    if (!PlayerPrefs.HasKey("Difficulty"))
    {
      PlayerPrefs.SetString("Difficulty", "Easy");
    }

    transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Сложность: " + Translate[PlayerPrefs.GetString("Difficulty")];
    transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Лучший результат: " + PlayerPrefs.GetInt("BestScore") + "$";
  }
}
