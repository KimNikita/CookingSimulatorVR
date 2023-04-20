using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MyInteractionManager
{
  override protected IEnumerator Check()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);
      if (leftController.action.ReadValue<float>() > 0.1 || rightController.action.ReadValue<float>() > 0.1)
      {
        StopCoroutine("Check");
        StartGame();
      }
    }
  }

  public void StartGame()
  {
    SceneManager.LoadScene(1);
  }
}
