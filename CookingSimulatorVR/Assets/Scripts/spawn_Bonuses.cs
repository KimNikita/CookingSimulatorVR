using System.Collections;
using UnityEngine;

public class spawn_Bonuses : MonoBehaviour
{
  public Transform spawnPoint;
  public GameObject bonus;
  Quaternion rotation;


  void Start()
  {
    rotation = bonus.transform.rotation;
    StartCoroutine(Spawn());
  }


  IEnumerator Spawn()
  {
    yield return new WaitForSeconds(35);
    Vector3 point = spawnPoint.position;
    Instantiate(bonus, point, rotation);
    StartCoroutine(Spawn());
  }
}
