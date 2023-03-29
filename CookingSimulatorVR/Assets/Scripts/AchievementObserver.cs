using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class AchievementObserver : MonoBehaviour, Observer
{    
    public List<Sprite> sprites;
    Dictionary<String, Sprite> _sprites_dictionary;
    private Image _achiev_image;
    void Start()
    {
        _achiev_image = gameObject.GetComponent<Image>();
        _achiev_image.enabled = false;
        _sprites_dictionary = new Dictionary<string, Sprite>(sprites.Count);
        foreach (var elem in sprites)
        {
            _sprites_dictionary.Add(elem.name, elem);
        }
    }
    public void HandleEvent(achievements ach)
    {
        // здесь обрабатывать события: выводить что-нибудь на UI и т.д.        
        if (ach == achievements.cheeseAchiev)
        {
            _achiev_image.sprite = _sprites_dictionary["cheeseAchiev"];
        }
        if (ach == achievements.trashBinAchiev)
        {
            _achiev_image.sprite = _sprites_dictionary["trashBinAchiev"];
        }
        if (ach == achievements.moneyAchiev)
        {
            _achiev_image.sprite = _sprites_dictionary["moneyAchiev"];
        }
        if (ach == achievements.orderAchiev)
        {
            _achiev_image.sprite = _sprites_dictionary["orderAchiev"];
        }
        if (ach == achievements.lolipopAchiev)
        {
            _achiev_image.sprite = _sprites_dictionary["lolipopAchiev"];
        }
        StartCoroutine(ShowAndHideImage());
        StartCoroutine(StartAndStopMusic());
    }

    IEnumerator ShowAndHideImage()
    {
        _achiev_image.enabled = true;
        yield return new WaitForSeconds(2);
        _achiev_image.enabled = false;
    }
    IEnumerator StartAndStopMusic()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        GetComponent<AudioSource>().Stop();
    }
}
