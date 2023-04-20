using System.Collections;
using UnityEngine;
using static GlobalVariables;

public class Red_Button : MonoBehaviour
{

  private Animator anim;
  public GameObject cup_with_anim;
  public GameObject cup;
  private Animator anim_cup;
  public static GameObject cup_clone;
  public GameObject point;
  private bool Active_Button_O = false;

  public AudioClip sound;
  public float volume = 1;

  private void Start()
  {
    anim = GetComponent<Animator>();
  }


  public void ActiveObj()
  {
    if (!hasDrink)
    {
      hasDrink = true;
      GetComponent<AudioSource>().PlayOneShot(sound, volume);
      anim.SetTrigger("Active_Button_Red");
      StartCoroutine(Wait());
    }
  }

  IEnumerator Wait()
  {
    yield return new WaitForSecondsRealtime(3);
    anim_cup = cup_with_anim.GetComponent<Animator>();
    anim_cup.SetBool("Active_Button_R", true);
    yield return new WaitForSecondsRealtime(1.8f);
    anim_cup.SetBool("Active_Button_R", false);
    yield return new WaitForSecondsRealtime(1f);
    cup_clone = Instantiate(cup, point.transform.position, cup.transform.rotation);
  }
}
