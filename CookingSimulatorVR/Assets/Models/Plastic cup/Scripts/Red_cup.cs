using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_cup : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void ActiveObj()
    {
        anim.SetTrigger("Active_Button_Red");
    }
}
