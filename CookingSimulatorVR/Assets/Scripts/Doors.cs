using UnityEngine;

public class Doors : MonoBehaviour
{
  public GameObject target;
  public AudioClip clip;
  public float volume = 0.3f;
  public void OpenDoors()
  {
    GetComponent<Animator>().SetBool("Open", true);
    AudioSource.PlayClipAtPoint(clip, target.transform.position, volume);
  }
}
