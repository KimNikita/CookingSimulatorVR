using System.Collections;
using UnityEngine;

public class RightScrollHowTo : MyInteractionManager
{
  public HowToPlayMenu howt_menu;
  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        howt_menu.rightScroll();
      }
    }
  }
}
