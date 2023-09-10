using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tour : MonoBehaviour
{
    public Image PageImage;
    public List<Sprite> Guides;
    public int PageNumber;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PointerEventData eventDataCurrentPos = new PointerEventData(EventSystem.current);
            eventDataCurrentPos.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPos, results);

            if (results.Count > 0)
            {
                if (results[0].gameObject.CompareTag("Tour"))
                {
                    startTouchPosition = Input.GetTouch(0).position;
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            PointerEventData eventDataCurrentPos = new PointerEventData(EventSystem.current);
            eventDataCurrentPos.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPos, results);

            if (results.Count > 0)
            {
                if (results[0].gameObject.CompareTag("Tour"))
                {
                    endTouchPosition = Input.GetTouch(0).position;

                    if (endTouchPosition.x < startTouchPosition.x)
                    {
                        NextPage();
                    }

                    if (endTouchPosition.x > startTouchPosition.x)
                    {
                        PreviousPage();
                    }
                }
            }
        }
    }
            

    private void NextPage()
    {
        PageNumber++;
        PageImage.sprite = Guides[PageNumber];
    }

    private void PreviousPage()
    {
        PageNumber--;
        PageImage.sprite = Guides[PageNumber];
    }
}
