using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HomeNav : MonoBehaviour
{
    public FirebaseAuthHandler auth;

    public GameObject sideBar;
    public GameObject barButton;

    private void Start()
    {
        auth = GameObject.FindGameObjectWithTag("Auth Handler").GetComponent<FirebaseAuthHandler>();
    }

    public void MoveToNextScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ShowSideBar()
    {
        sideBar.SetActive(true);
        barButton.SetActive(false);
    }

    public void HideSideBar()
    {
        if (sideBar.activeInHierarchy == true)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PointerEventData eventDataCurrentPos = new PointerEventData(EventSystem.current);
                eventDataCurrentPos.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPos, results);

                if (results.Count > 0)
                {
                    if (!results[0].gameObject.CompareTag("Sidebar") && !results[0].gameObject.CompareTag("SidebarButtons"))
                    {
                        sideBar.SetActive(false);
                        barButton.SetActive(true);
                    }
                }
            }
        }

    }

    private void Update()
    {
        HideSideBar();
    }

    public void LogOut()
    {
        auth.SignOut();
    }
}
