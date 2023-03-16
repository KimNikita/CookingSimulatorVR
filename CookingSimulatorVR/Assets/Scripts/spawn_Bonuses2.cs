using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_Bonuses2 : MonoBehaviour
{
  public Transform spawnPoint;
  public GameObject bonus;
  Quaternion rotation;
  //[SerializeField] GameObject bonus;


  void Start()
  {
    rotation = bonus.transform.rotation;
    StartCoroutine(Spawn());
  }


  IEnumerator Spawn()
  {
    yield return new WaitForSeconds(40);
    Vector3 point = spawnPoint.position;
    Instantiate(bonus, point, rotation);
    StartCoroutine(Spawn());
  }
}
