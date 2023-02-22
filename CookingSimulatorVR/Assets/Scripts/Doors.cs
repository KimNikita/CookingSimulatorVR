using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
  public void OpenDoors()
  {
    gameObject.GetComponent<Animator>().SetBool("Open", true);
  }
}
