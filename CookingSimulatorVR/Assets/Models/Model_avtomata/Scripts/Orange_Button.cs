using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange_Button : MonoBehaviour
{
    private Animator anim;
    public GameObject cup_with_anim;
    public GameObject cup;
    private Animator anim_cup;
    public static GameObject cup_clone;
    public GameObject point;
    private bool Active_Button_O = false;
    private void Start()
    {
        anim = GetComponent<Animator>(); 
    }


    public void ActiveObj()
    {
        anim.SetTrigger("Active_Button_Orange");
        StartCoroutine(Wait());    
  }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);
        anim_cup = cup_with_anim.GetComponent<Animator>();
        anim_cup.SetBool("Active_Button_O", true);
        yield return new WaitForSecondsRealtime(1.8f);
        anim_cup.SetBool("Active_Button_O", false);
        yield return new WaitForSecondsRealtime(0.4f);
        cup_clone = Instantiate(cup, point.transform.position, cup.transform.rotation);
  }
}
