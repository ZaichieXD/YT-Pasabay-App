using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidButtonTracker : MonoBehaviour
{
    public ChatHandler chatHandler;
    public FirebaseDatabaseHandler database;

    public ProfileNavigation profile;

    void Start()
    {
        DontDestroyOnLoad(this);
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<FirebaseDatabaseHandler>();
    }

    public void FindChatHandler()
    {
        chatHandler = GameObject.FindGameObjectWithTag("Chat Handler").GetComponent<ChatHandler>();
    }

    public void FindProfileNavigation()
    {
        profile = GameObject.FindGameObjectWithTag("Profile").GetComponent<ProfileNavigation>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Application.Quit();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                Application.Quit();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SceneManager.LoadScene(1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                chatHandler.DeleteAllChildMessage();
                database.DeleteAMessage();
                SceneManager.LoadScene(1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                StartCoroutine(database.GetComponent<FirebaseDatabaseHandler>().UpdateProfilePicIdDatabase(profile.pictureIndex));
                StartCoroutine(database.GetComponent<FirebaseDatabaseHandler>().GetProfilePicId());
                SceneManager.LoadScene(1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                SceneManager.LoadScene(1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
