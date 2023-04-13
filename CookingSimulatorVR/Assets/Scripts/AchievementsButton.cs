using System.Collections;
using UnityEngine;

public class AchievementsButton : MyInteractionManager
{
  public GameObject achievPanel;

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        Achievements();
      }
    }
  }

  public void Achievements()
  {
    Instantiate(achievPanel);
    transform.parent.gameObject.SetActive(false);
  }
}

