using System.Collections;
using TMPro;
using UnityEngine;

public class ResetButton : MyInteractionManager
{
  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        ResetRecord();
      }
    }
  }

  private void ResetRecord()
  {
    var ach_list = new string[] { "cheeseAchiev", "lolipopAchiev", "moneyAchiev", "orderAchiev", "trashBinAchiev" };
    foreach (string achiev_key in ach_list)
    {
      PlayerPrefs.DeleteKey(achiev_key);
    }
    PlayerPrefs.SetInt("BestScore", 0);
    transform.parent.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Лучший результат: 0$";
  }
}
