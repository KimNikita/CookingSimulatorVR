using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Button : MonoBehaviour
{

    private Animator anim;
    public GameObject cup;
    private Animator anim_cup;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim_cup = cup.GetComponent<Animator>();
    }


    public void ActiveObj()
    {
        Instantiate(cup, new Vector3(-1.773f, 1.479f, -0.847f), cup.transform.rotation);
        anim.SetTrigger("Active_Button_Red");
        StartCoroutine(Wait());
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);
        anim_cup.SetTrigger("Cup_Red");
    }
}
