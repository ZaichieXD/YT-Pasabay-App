using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour
{
    public GameObject ratesAndServices;
    public GameObject messages;
    public GameObject notifications;

    public void OpenMessages()
    {
        messages.SetActive(true);
        ratesAndServices.SetActive(false);
        notifications.SetActive(false);
    }

    public void OpenRatesAndServices()
    {
        messages.SetActive(false);
        ratesAndServices.SetActive(true);
        notifications.SetActive(false);
    }

    public void OpenNotification()
    {
        messages.SetActive(false);
        ratesAndServices.SetActive(false);
        notifications.SetActive(true);
    }

    public void AddFormPopUp(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ReturnToMenu(int index)
    {
        SceneManager.LoadScene(index);
    }
}
