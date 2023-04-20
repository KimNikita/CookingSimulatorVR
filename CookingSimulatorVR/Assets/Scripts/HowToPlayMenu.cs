using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HowToPlayMenu : MonoBehaviour
{
  public List<Sprite> sprites;
  private Image image;
  private int page_number;
  void Start()
  {
    image = transform.GetChild(3).GetComponent<Image>();
    page_number = 0;
    SetImages(page_number);
  }
  private void SetImages(int page_number)
  {
    image.sprite = sprites[page_number];
  }
  public void leftScroll()
  {
    if (page_number > 0)
    {
      --page_number;
      SetImages(page_number);
    }
  }
  public void rightScroll()
  {
    if (page_number < sprites.Count - 1)
    {
      ++page_number;
      SetImages(page_number);
    }
  }
  public void close()
  {
    GameObject.FindGameObjectWithTag("Menu").transform.GetChild(0).gameObject.SetActive(true);
    Destroy(gameObject);
  }
}
