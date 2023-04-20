using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToButton : MyInteractionManager
{
  public GameObject howToPanel;

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        HowTo();
      }
    }
  }

  public void HowTo()
  {
    Instantiate(howToPanel);
    transform.parent.gameObject.SetActive(false);
  }
}
