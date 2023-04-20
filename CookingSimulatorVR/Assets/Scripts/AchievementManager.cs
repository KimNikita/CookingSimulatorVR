using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class AchievementManager : MonoBehaviour
{
  public List<Sprite> sprites;
  Dictionary<String, Sprite> _sprites_dictionary;
  Image _achiev_image;
  AudioSource _audio_source;
  static AchievementManager _instance;

  int _cheeseNumber = 0;
  private AchievementManager()
  {
  }
  static public AchievementManager GetInstance()
  {
    return _instance;
  }
  void Start()
  {
    _instance = this;
    _achiev_image = gameObject.GetComponent<Image>();
    _achiev_image.enabled = false;
    _sprites_dictionary = new Dictionary<string, Sprite>(sprites.Count);
    foreach (var elem in sprites)
    {
      _sprites_dictionary.Add(elem.name, elem);
    }
    _audio_source = GetComponent<AudioSource>();
  }
  public void HandleEvent(achievements ach)
  {
    // здесь обрабатывать события: выводить что-нибудь на UI и т.д.        
    if (ach == achievements.cheeseAchiev)
    {
      _achiev_image.sprite = _sprites_dictionary["cheeseAchiev"];
      PlayerPrefs.SetString("cheeseAchiev", "cheeseAchiev");
    }
    if (ach == achievements.trashBinAchiev)
    {
      _achiev_image.sprite = _sprites_dictionary["trashBinAchiev"];
      PlayerPrefs.SetString("trashBinAchiev", "trashBinAchiev");
    }
    if (ach == achievements.moneyAchiev)
    {
      _achiev_image.sprite = _sprites_dictionary["moneyAchiev"];
      PlayerPrefs.SetString("moneyAchiev", "moneyAchiev");
    }
    if (ach == achievements.orderAchiev)
    {
      _achiev_image.sprite = _sprites_dictionary["orderAchiev"];
      PlayerPrefs.SetString("orderAchiev", "orderAchiev");
    }
    if (ach == achievements.lolipopAchiev)
    {
      _achiev_image.sprite = _sprites_dictionary["lolipopAchiev"];
      PlayerPrefs.SetString("lolipopAchiev", "lolipopAchiev");
    }
    StartCoroutine(ShowAndHideImage());
    StartCoroutine(StartAndStopMusic());
  }

  IEnumerator ShowAndHideImage()
  {
    _achiev_image.enabled = true;
    yield return new WaitForSeconds(4);
    _achiev_image.enabled = false;
  }
  IEnumerator StartAndStopMusic()
  {
    _audio_source.Play();
    yield return new WaitForSeconds(2);
    _audio_source.Stop();
  }
}
