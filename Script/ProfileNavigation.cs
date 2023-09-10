using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileNavigation : MonoBehaviour
{
    private GameObject databaseContainer; // Reference to the Database Object
    private AndroidButtonTracker tracker;

    public List<Sprite> profilePics;
    public Image profilePicture;
    public int pictureIndex;
    public TMP_Text fullName; // Full Name UI

    void Start()
    {
        // Find the Database Object
        databaseContainer = GameObject.FindGameObjectWithTag("Database");
        tracker = GameObject.FindGameObjectWithTag("Button Tracker").GetComponent<AndroidButtonTracker>();

        tracker.FindProfileNavigation();

        // Assign the value of the Database Object to the UI
        fullName.text = databaseContainer.GetComponent<FirebaseDatabaseHandler>().fullName;
        pictureIndex = int.Parse(databaseContainer.GetComponent<FirebaseDatabaseHandler>().profileId);
        profilePicture.sprite = profilePics[int.Parse(databaseContainer.GetComponent<FirebaseDatabaseHandler>().profileId)];
    }

    public void ChangeImage(string direction)
    {
        if (direction == "forward")
        {
            pictureIndex++;

            if (pictureIndex >= profilePics.Count - 1)
            {
                pictureIndex = profilePics.Count - 1;
                Debug.Log(profilePics.Count);
            }
        }
        else if (direction == "back")
        {
            pictureIndex--;

            if (pictureIndex < 0)
            {
                pictureIndex = 0;
            }
        }
        else
        {
            Debug.LogError("Unknown Value");
        }

        profilePicture.sprite = profilePics[pictureIndex];
    }

    public void CloseWindow()
    {
        SceneManager.LoadScene(1);
    }
}
