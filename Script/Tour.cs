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

    // Gives the function swipe to the tour from the main menu
    void Update()
    {
        // If input touchCount is more than 0 then the system will detect that as a touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Checks what position in the screen the user is pressing and gets the coordinates
            PointerEventData eventDataCurrentPos = new PointerEventData(EventSystem.current); // Instantiates a PointerEventData where the data if the touch is stored
            eventDataCurrentPos.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

            // Creates a List of results of all the raycast
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPos, results);

            // if the object hit by the raycast has a tag called "Tour"
            if (results.Count > 0)
            {
                if (results[0].gameObject.CompareTag("Tour"))
                {
                    startTouchPosition = Input.GetTouch(0).position; // Record the position where it started for later use
                }
            }
        }

        // if the user lifts his hand off the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // Get the position where the touch has ended
            PointerEventData eventDataCurrentPos = new PointerEventData(EventSystem.current);
            eventDataCurrentPos.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

            // Store it in a Raycast lists variable
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPos, results);

            // Checks if the touch is still in the "Tour"
            if (results.Count > 0)
            {
                if (results[0].gameObject.CompareTag("Tour"))
                {
                    endTouchPosition = Input.GetTouch(0).position;
                    // if it is in the "Tour"
                    if (endTouchPosition.x < startTouchPosition.x)
                    {
                        NextPage(); // Move Left
                    }

                    if (endTouchPosition.x > startTouchPosition.x)
                    {
                        PreviousPage(); // Move Right
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
