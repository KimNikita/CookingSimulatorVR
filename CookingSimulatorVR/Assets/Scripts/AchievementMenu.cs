using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchievementMenu : MonoBehaviour
{
    public List<Sprite> complete_achiev_sprites;
    public List<Sprite> incomplete_achiev_sprites;
    private Transform images_grid;
    private int page_number;
    const int max_page_number = 2;
    const int max_image_number = 3;
    void Start()
    {
        images_grid = transform.GetChild(3);
        page_number = 0;
        SetImages(page_number);

        EventTrigger leftScrollTrigger = transform.GetChild(0).gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry leftScrollPointerDown = new EventTrigger.Entry();
        leftScrollPointerDown.eventID = EventTriggerType.PointerDown;
        leftScrollPointerDown.callback.AddListener((eventData) => { leftScroll(); });
        leftScrollTrigger.triggers.Add(leftScrollPointerDown);

        EventTrigger rightScrollTrigger = transform.GetChild(1).gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry rightScrollPointerDown = new EventTrigger.Entry();
        rightScrollPointerDown.eventID = EventTriggerType.PointerDown;
        rightScrollPointerDown.callback.AddListener((eventData) => { rightScroll(); });
        rightScrollTrigger.triggers.Add(rightScrollPointerDown);

        EventTrigger closeTrigger = transform.GetChild(2).gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry closePointerDown = new EventTrigger.Entry();
        closePointerDown.eventID = EventTriggerType.PointerDown;
        closePointerDown.callback.AddListener((eventData) => { close(); });
        closeTrigger.triggers.Add(closePointerDown);
    }
    /*private void createNewTrigger(GameObject g, EventTriggerType triggerType, delegate*<void> function) // нужно обновить C# до 9 версии
    {
        EventTrigger trigger = g.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((eventData) => { function(); });
        trigger.triggers.Add(entry);
    }*/
    private void clearImages()
    {
        for(int i = 0; i < max_image_number; ++i)
        {
            images_grid.transform.GetChild(i).GetComponent<Image>().sprite = null;
        }
    }
    private void SetImages(int page_number) 
    {
        int i, grid_child_index;
        Image image;
        for (i = page_number * max_image_number, grid_child_index = 0;
             i < max_image_number * (1 + page_number) && i < complete_achiev_sprites.Count;
             ++i, ++grid_child_index)
        {
            image = images_grid.transform.GetChild(grid_child_index).GetComponent<Image>();
            image.enabled = true;
            //image.sprite = PlayerPrefs.HasKey(complete_achiev_sprites[i].name) ? complete_achiev_sprites[i] : incomplete_achiev_sprites[i];
            image.sprite = complete_achiev_sprites[i];
        }
        for (; i < max_image_number * (1 + page_number); ++i, ++grid_child_index) // если остались ещё пустые места для достижений - спрятать их
        {
            images_grid.transform.GetChild(grid_child_index).GetComponent<Image>().enabled = false;
        }
    }
    private void leftScroll()
    {
        if (page_number > 0)
        {
            clearImages();
            --page_number;
            SetImages(page_number);
        }
    }
    private void rightScroll()
    {
        Debug.Log(page_number);
        Debug.Log(max_page_number);
        if (page_number < max_page_number - 1)
        {
            clearImages();
            ++page_number;
            SetImages(page_number);
        }
    }
    private void close()
    {        
        Destroy(this.gameObject);
    }
}
