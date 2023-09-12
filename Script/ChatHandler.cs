using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatHandler : MonoBehaviour
{
    public TMP_InputField message;
    public FirebaseDatabaseHandler databaseHandler;
    public TMP_Text messagePrefab;
    public Transform contentParent;
    public AndroidButtonTracker tracker;

    // public bool isEnabled = false;
    public TMP_Text usernameText;

    void Start()
    {
        databaseHandler = GameObject.FindGameObjectWithTag("Database").GetComponent<FirebaseDatabaseHandler>();
        tracker = GameObject.FindGameObjectWithTag("Button Tracker").GetComponent<AndroidButtonTracker>();
        //isEnabled = true;
        databaseHandler.FindChatHandler();
        //databaseHandler.PostMessage();
        tracker.FindChatHandler();

        usernameText.text = "Admin";
        StartCoroutine(databaseHandler.GetAMessage());
    }

    // Writes a message and instantiate a message box
    public void WriteMessage(List<string> message, int numberOfInstance, List<string> sender)
    {
        for (int index = 0; index < numberOfInstance; index++)
        {
            TMP_Text messageInstance = Instantiate(messagePrefab, contentParent, false);
            messageInstance.text = message[index];
            messageInstance.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = sender[index];
        }
    }

    // Writes a message and instantiate a message box
    public void WriteMessageListener(string message, string sender)
    {
        TMP_Text messageInstance = Instantiate(messagePrefab, contentParent, false);
        messageInstance.text = message;
        messageInstance.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = sender;

        this.message.text = "";
    }

    public void SendFuntion()
    {
        StartCoroutine(databaseHandler.SendAMessage(message.text));
    }

    public void DeleteAllChildMessage()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

    }

    /*void OnDisable()
    {
        databaseHandler.DeleteAMessage();
    }*/
}
