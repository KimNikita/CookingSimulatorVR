using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
  public TextMeshProUGUI ScoreText;

  void Start()
  {
    GlobalVariables.scoreValue = 100;
    ScoreText.text = GlobalVariables.scoreValue + "$";
  }

  // Update is called once per frame
  void Update()
  {

  }
}
