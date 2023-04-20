using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftScrollHowTo : MyInteractionManager
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
        howt_menu.leftScroll();
      }
    }
  }
}
