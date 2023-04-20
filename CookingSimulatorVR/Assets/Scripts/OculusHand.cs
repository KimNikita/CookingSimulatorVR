using UnityEngine;

public class OculusHand : MonoBehaviour
{

  public Transform GetTransform()
  {
    return transform;
  }

  public Vector3 GetPosition()
  {
    return transform.position;
  }

  public Quaternion GetRotation()
  {
    return transform.rotation;
  }

  public bool HasChildren()
  {
    return transform.childCount > 3;
  }

  public Transform GetChild()
  {
    if (transform.childCount > 3)
    {
      return transform.GetChild(3);
    }
    return null;
  }

  public string GetChildTag()
  {
    if (transform.childCount > 3)
    {
      return gameObject.transform.GetChild(3).tag;
    }
    return "None";
  }
}
