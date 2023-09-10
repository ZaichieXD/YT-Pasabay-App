using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PackageMonitor : MonoBehaviour
{
    FirebaseDatabaseHandler databaseHandler;
    public FirebaseAuthHandler firebaseAuthHandler;
    public FirebaseStorageHandler storageHandler;
    public DatabaseReference DBreference;

    public GameObject formTemplate;
    public Transform formParent;

    public GameObject containerTab;
    public GameObject formTab;

    public List<GameObject> fillUpForms;
    public List<GameObject> mainFillUpForms;

    public TMP_Text trackingNumber;
    public TMP_Text shopName;
    public TMP_Text buyerName;
    public TMP_Text sellerName;
    public TMP_Text description;
    public TMP_Text amount;
    public TMP_Text destination;
    public TMP_Text dropdate;
    public TMP_Text status;
    public TMP_Text eta;
    public TMP_Text currentDA;
    public RawImage image;
    public string updatedStatus;
    public string updatedDA;
    public string trackingNumberValue;

    private Animator flipAnimator;
    public Animator tabAnimator;

    public TMP_Text noDataText;

    private void Start()
    {
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        databaseHandler = GameObject.FindGameObjectWithTag("Database").GetComponent<FirebaseDatabaseHandler>();
        firebaseAuthHandler = GameObject.FindGameObjectWithTag("Auth Handler").GetComponent<FirebaseAuthHandler>();
        storageHandler = GameObject.FindGameObjectWithTag("Storage").GetComponent<FirebaseStorageHandler>();
        databaseHandler.FindPackageMonitor();
        tabAnimator.SetBool("Switched", true);
        //s\fillUpForms.Add(formTemplate);

        //Gets the total number of forms and gets the data from the database
        SwitchActiveTab("Pending");
    }

    public void SwitchActiveTab(string status)
    {
        StartCoroutine(GetTotalForms(status));
        StartCoroutine(databaseHandler.GetFormData(status));
    }


    private IEnumerator GetTotalForms(string status)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;
        var DBTaskData = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).Child("Tracking_Numbers").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTaskData.IsCompleted);

        if (DBTaskData.Exception != null)
        {
            // If there is and error
            UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTaskData.Exception);
        }
        else if (DBTaskData.Result.Value == null)
        {
            // If there is no data Send Error
            noDataText.gameObject.SetActive(true);
        }
        else
        {
            if (noDataText.gameObject.activeInHierarchy == true)
            {
                noDataText.gameObject.SetActive(false);
            }

            if (formTemplate.activeInHierarchy == false)
            {
                formTemplate.SetActive(true);
            }

            // Load the data
            DataSnapshot DBTaskDataSnapshot = DBTaskData.Result;

            var DBTaskDataStatus = DBreference.Child("TrackingNumbers").GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTaskDataStatus.IsCompleted);

            if (DBTaskDataStatus.Exception != null)
            {
                // If there is and error
                UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTaskDataStatus.Exception);
            }
            else if (DBTaskDataStatus.Result.Value == null)
            {
                // If there is no data Send Error
                UnityEngine.Debug.Log("No Data");
            }
            else
            {
                DataSnapshot statusSnapshot = DBTaskDataStatus.Result;

                for (int i = 0; i < fillUpForms.Count; i++)
                {
                    Destroy(fillUpForms[i].gameObject);
                }

                fillUpForms.Clear();

                foreach (var child in DBTaskDataSnapshot.Children)
                {
                    if (statusSnapshot.Child(child.Value.ToString()).Child("Status").Value.ToString() == status)
                    {
                        AddGetForms();
                    }
                }
            }

            formTemplate.SetActive(false);

        }

    }

    public void AddGetForms()
    {
        GameObject tempObj = Instantiate(formTemplate, formParent, false);

        fillUpForms.Add(tempObj);
    }

    public void AssignValues(string shopName, string trackingNumber, string description, string amount, string dateDropped, string eta, string buyerName, string sellerName, string destination, string status, string currentDA, int index)
    {
        TMP_Text[] filledUpFormsFront = fillUpForms[index].transform.GetChild(1).GetComponentsInChildren<TMP_Text>();
        TMP_Text[] filledUpFormsBack = fillUpForms[index].transform.GetChild(0).GetComponentsInChildren<TMP_Text>();
        filledUpFormsFront[0].text = eta;
        filledUpFormsFront[1].text = description;
        filledUpFormsFront[2].text = buyerName;
        filledUpFormsFront[3].text = amount;
        filledUpFormsFront[4].text = status;
        filledUpFormsFront[5].text = trackingNumber;

        filledUpFormsBack[0].text = sellerName;
        filledUpFormsBack[1].text = shopName;
        filledUpFormsBack[2].text = destination;
        filledUpFormsBack[3].text = dateDropped;
        filledUpFormsBack[4].text = currentDA;
    }

    public void TrackPackageForSeller()
    {
        TMP_InputField[] trackingNumberText = EventSystem.current.currentSelectedGameObject.GetComponentsInChildren<TMP_InputField>();

        StartCoroutine(databaseHandler.GetDataForSellers(trackingNumberText[0].text));

        formTab.SetActive(true);
        containerTab.SetActive(false);

    }

    public void AssignValuesToForm(string shopName, string trackingNumber, string description, string amount, string dateDropped, string eta, string buyerName, string sellerName, string destination, string status, string currentDA)
    {
        this.shopName.text = shopName;
        this.trackingNumber.text = trackingNumber;
        this.description.text = description;
        this.amount.text = amount;
        this.dropdate.text = dateDropped;
        this.eta.text = eta;
        this.buyerName.text = buyerName;
        this.sellerName.text = sellerName;
        this.destination.text = destination;
        this.status.text = status;
        this.currentDA.text = currentDA;
        storageHandler.DownloadDataFromStorage(trackingNumber, image);
    }

    public void SwitchDisplay()
    {
        flipAnimator = EventSystem.current.currentSelectedGameObject.GetComponent<Animator>();

        if (flipAnimator.GetLayerName(0) == "Flip Layer")
        {

            if (flipAnimator.GetBool("Flip") == false)
            {
                flipAnimator.SetBool("Flip", true);
            }
            else
            {
                flipAnimator.SetBool("Flip", false);
            }
        }
    }

    public void SwitchTab()
    {
        if (tabAnimator != null)
        {
            tabAnimator.SetBool("Switched", false);
        }

        tabAnimator = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Animator>();

        if (tabAnimator.GetLayerName(0) == "Line Layer")
        {
            if (tabAnimator.GetBool("Switched") == false)
            {
                tabAnimator.SetBool("Switched", true);
            }
        }
    }

    public void TakeMeHomeToThePlaceIBelong()
    {
        SceneManager.LoadScene(1);
    }
    public void GoBack()
    {
        formTab.SetActive(false);
        containerTab.SetActive(true);
    }
}
