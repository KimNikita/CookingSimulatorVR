using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ToMenu : MyInteractionManager
{
  // Start is called before the first frame update

  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        GoToMenu();
      }
    }
  }

  public void GoToMenu()
  {
    GlobalVariables.scoreValue = 100;
    GlobalVariables.end = false;
    SceneManager.LoadScene(0);
  }
}
