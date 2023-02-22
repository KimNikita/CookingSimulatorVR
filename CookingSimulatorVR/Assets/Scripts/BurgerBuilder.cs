using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerBuilder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger Enter");
        other.gameObject.transform.parent = gameObject.transform;
    }
}
