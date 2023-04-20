using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MyInteractionManager
{
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
