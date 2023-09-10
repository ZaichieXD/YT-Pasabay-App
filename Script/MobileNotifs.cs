using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Messaging;
using TMPro;

public class MobileNotifs : MonoBehaviour
{
    private string devicetoken;
    private FirebaseDatabaseHandler database;

    private void Start()
    {
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<FirebaseDatabaseHandler>();

        FirebaseMessaging.TokenReceived += OnTokenReceived;
        FirebaseMessaging.MessageReceived += OnMessageReceived;

        StartCoroutine(GetFirebaseToken());
    }

    //Get firebase messaging token
    private IEnumerator GetFirebaseToken()
    {
        var token = FirebaseMessaging.GetTokenAsync();

        yield return new WaitUntil(predicate: () => token.IsCompleted);

        if (token.Exception != null)
        {
            Debug.LogError(message: $"Failed to register firebase token {token.Exception}");
        }
        else
        {
            devicetoken = token.Result;
            StartCoroutine(database.UpdateTokenDatabase(devicetoken));
        }
    }

    public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
    }

    private void OnDestroy()
    {
        FirebaseMessaging.TokenReceived -= OnTokenReceived;
        FirebaseMessaging.MessageReceived -= OnMessageReceived;
    }

    public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);
    }


}
