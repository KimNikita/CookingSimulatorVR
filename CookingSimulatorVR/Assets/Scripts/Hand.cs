using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
  public bool HasChildren()
  {
    return gameObject.transform.childCount != 0;
  }

  public Vector3 GetPosition()
  {
    return gameObject.transform.position;
  }

  public Transform GetTransform()
  {
    return gameObject.transform;
  }

  public string GetChildTag()
  {
    if (gameObject.transform.childCount != 0)
    {
      return gameObject.transform.GetChild(0).tag;
    }
    return "None";
  }

  public Quaternion GetRotation()
  {
    return gameObject.transform.rotation;
  }
}
