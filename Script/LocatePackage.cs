using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;

public class LocatePackage : MonoBehaviour
{
    public FirebaseDatabaseHandler databaseHandler;
    public FirebaseAuthHandler firebaseAuthHandler;
    public FirebaseStorageHandler storageHandler;
    public DatabaseReference DBreference;
    public bool noData = false;

    public TMP_InputField trackingNumberInput;

    public TMP_Text shopName;
    public TMP_Text trackingNumber;
    public TMP_Text description;
    public TMP_Text amount;
    public TMP_Text dateDropped;
    public TMP_Text status;
    public TMP_Text currentDA;
    public RawImage image;

    public GameObject recordformTab;
    public GameObject trackingInputTab;

    public GameObject screenPopUp;

    public void Start()
    {
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        databaseHandler = GameObject.FindGameObjectWithTag("Database").GetComponent<FirebaseDatabaseHandler>();
        firebaseAuthHandler = GameObject.FindGameObjectWithTag("Auth Handler").GetComponent<FirebaseAuthHandler>();
        storageHandler = GameObject.FindGameObjectWithTag("Storage").GetComponent<FirebaseStorageHandler>();
        databaseHandler.FindPackageLocator();
    }

    public void TrackPackageUser()
    {
        StartCoroutine(databaseHandler.GetDataForUsers(this.trackingNumberInput.text));
        if (noData == true)
        {
            recordformTab.SetActive(true);
            trackingInputTab.SetActive(false);
            
        }
    }

    public void EndExecution()
    {
        screenPopUp.SetActive(true);
        noData = false;
    }

    public void Close()
    {
        screenPopUp.SetActive(false);
    }

    public void GoBack()
    {
        recordformTab.SetActive(false);
        trackingInputTab.SetActive(true);
    }

    public void AssignValues(string shopName, string trackingNumber, string description, string amount, string dateDropped, string status, string currentDA)
    {
        this.shopName.text = shopName;
        this.trackingNumber.text = trackingNumber;
        this.description.text = description;
        this.amount.text = amount;
        this.dateDropped.text = dateDropped;
        this.status.text = status;
        this.currentDA.text = currentDA;
        storageHandler.DownloadDataFromStorage(trackingNumber, image);
    }
}
