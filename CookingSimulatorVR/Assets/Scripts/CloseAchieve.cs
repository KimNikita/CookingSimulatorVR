using System.Collections;
using UnityEngine;

public class CloseAchieve : MyInteractionManager
{
  public AchievementMenu ach_menu;
  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        ach_menu.close();
      }
    }
  }
}
