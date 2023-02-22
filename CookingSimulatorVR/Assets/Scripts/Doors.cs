using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
  public GameObject target;
  public AudioClip clip;
  public float volume = 0.5f;
  public void OpenDoors()
  {
    gameObject.GetComponent<Animator>().SetBool("Open", true);
    AudioSource.PlayClipAtPoint(clip, target.transform.position, volume);
  }
}
