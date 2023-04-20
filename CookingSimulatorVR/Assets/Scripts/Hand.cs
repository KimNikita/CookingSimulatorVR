using UnityEngine;

public class Hand : MonoBehaviour
{
  private static Hand instance;

  private void Start()
  {
    instance = this;
  }

  public static bool HasChildren()
  {
    return instance.transform.childCount != 0;
  }

  public static Vector3 GetPosition()
  {
    return instance.transform.position;
  }

  public static Transform GetTransform()
  {
    return instance.transform;
  }

  public static string GetChildTag()
  {
    if (instance.transform.childCount != 0)
    {
      return instance.transform.GetChild(0).tag;
    }
    return "None";
  }

  public static Quaternion GetRotation()
  {
    return instance.transform.rotation;
  }
}
