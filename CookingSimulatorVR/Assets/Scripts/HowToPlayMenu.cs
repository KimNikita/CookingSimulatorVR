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
    private void SetImages(int page_number)
    {
        image.sprite = sprites[page_number];
    }
    private void leftScroll()
    {
        if (page_number > 0)
        {
            --page_number;
            SetImages(page_number);
        }
    }
    private void rightScroll()
    {
        if (page_number < sprites.Count - 1)
        {
            ++page_number;
            SetImages(page_number);
        }
    }
    private void close()
    {
        Destroy(this.gameObject);
    }
}
