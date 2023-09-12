using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using Firebase;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FirebaseDatabaseHandler : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus; // Is the Account Valid
    public DatabaseReference DBreference; // gateway to the database
    public AccountHandler accountHandler;  // Account Handler Object
    public FirebaseAuthHandler firebaseAuthHandler; // Database Handler Object
    public FormCreator formCreator; // Form Template Creator
    public PackageMonitor packageMonitor;
    public LocatePackage packageLocator;
    public FirebaseStorageHandler storageHandler;
    public ChatHandler chatHandler;


    [Header("Database Converted Values")]

    public string fullName; // Full Name from the database
    public string fullAddress; // Full Address from the database
    public string phoneNumber; // Phone Number from the database  
    public string shopName; // Shop Name from database

    public string userRole; // User Role from database
    public string profileId; // Profile Id from database

    public List<string> messagesList;
    public List<string> senderList;
    private string databaseKey;
    public DatabaseReference DBReferenceN;

    public TMP_Text userNameText;

    string buyerName;
    string address;
    string otherDetails;
    string description;

    long clientPhoneNumber;
    int length;
    int width;
    int height;
    int weight;
    public int totalAmount;

    public int trackingNumber;

    // Initialize Database
    private void Awake()
    {
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(this);
    }

    // Finds an Object with a tag Form Creator
    public void FindFormCreator()
    {
        formCreator = GameObject.FindGameObjectWithTag("Form Creator").GetComponent<FormCreator>();
    }

    // Finds an Object with a tag Package Monitor
    public void FindPackageMonitor()
    {
        packageMonitor = GameObject.FindGameObjectWithTag("Package Monitor").GetComponent<PackageMonitor>();
    }

    // Finds an Object with a tag Package Locator
    public void FindPackageLocator()
    {
        packageLocator = GameObject.FindGameObjectWithTag("Package Locator").GetComponent<LocatePackage>();
    }

    // Finds an Object with a tag Chat Handler
    public void FindChatHandler()
    {
        chatHandler = GameObject.FindGameObjectWithTag("Chat Handler").GetComponent<ChatHandler>();
    }

    #region Login and Sign up

    // Upload User to the database
    public void SaveDataButton()
    {
        // Updates the UserName in both User Authentication Database and Realtime Database
        StartCoroutine(UpdateUserNameAuth(accountHandler.fullName.text));
        StartCoroutine(UpdateUserNameDatabase(accountHandler.fullName.text));

        // Updates The Database with the values from the input fields
        StartCoroutine(UpdateAddressDatabase(accountHandler.fullAddress.text));
        StartCoroutine(UpdatePhoneNumberDatabase(accountHandler.phoneNumber.text));
        StartCoroutine(UpdateShopNameDatabase(accountHandler.shopName.text));
        StartCoroutine(UpdateUserPrivilegeDatabase("user"));
        StartCoroutine(UpdateProfilePicIdDatabase(0));


        // Call the LoadAuthentication Function from Account Handler
        accountHandler.LoadToLoginScreen();
    }

    // Updates The UserName in the User Authentication System Database
    private IEnumerator UpdateUserNameAuth(string _username)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;

        //Create user profile and set Username

        UserProfile profile = new UserProfile { DisplayName = _username };

        // Create the profile
        var ProfileTask = firebaseAuthHandler.User.UpdateUserProfileAsync(profile);

        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            // Error
        }
        else
        {
            // Username Update Success
        }
    }

    public IEnumerator UpdateProfilePicIdDatabase(int _picId)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;
        var DBTask = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).Child("profile_id").SetValueAsync(_picId);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
        }
        else
        {
            // Username Update Success
        }
    }

    // Updates The UserName in the Database
    private IEnumerator UpdateUserNameDatabase(string _username)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;
        var DBTask = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).Child("full_name").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
        }
        else
        {
            // Username Update Success
        }
    }

    // Updates The Address in the Database
    private IEnumerator UpdateAddressDatabase(string _address)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;
        var DBTask = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).Child("full_address").SetValueAsync(_address);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    // Updates The Phone Number in the Database
    private IEnumerator UpdatePhoneNumberDatabase(string _phoneNumber)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;

        var DBTask = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).Child("phone_number").SetValueAsync(_phoneNumber);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Phone Number
        }
    }

    // Updates The Shop Name in Database
    private IEnumerator UpdateShopNameDatabase(string _shopName)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;

        var DBTask = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).Child("shop_name").SetValueAsync(_shopName);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Phone Number
        }
    }

    // Set user privileges in firebase database
    private IEnumerator UpdateUserPrivilegeDatabase(string _user_privilege)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;

        var DBTask = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).Child("user_privilege").SetValueAsync(_user_privilege);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Phone Number
        }
    }

    // Update token in database
    public IEnumerator UpdateTokenDatabase(string _token)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;

        var DBTask = DBreference.Child("DeviceTokens").Child("user").Child(fullName).SetValueAsync(_token);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
            Debug.Log(DBTask.Exception);
        }
        else
        {
            //Update Phone Number
            Debug.Log("Token Updated");
        }
    }

    // Load User Data to a file
    public IEnumerator LoadUserData()
    {
        // Get the Current Account in Auth
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;

        // Load the Account with the same ID in Auth
        var DBTask = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).GetValueAsync();

        // Wait Until the Values are retrieved
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            // If there is and error
            UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTask.Exception);
        }
        else if (DBTask.Result.Value == null)
        {
            // If there is no data Send Error
            UnityEngine.Debug.Log("No Data");
        }
        else
        {
            // Load the data
            DataSnapshot snapshot = DBTask.Result;

            // Assign the value from the database to these variables
            fullName = snapshot.Child("full_name").Value.ToString();
            fullAddress = snapshot.Child("full_address").Value.ToString();
            phoneNumber = snapshot.Child("phone_number").Value.ToString();
            shopName = snapshot.Child("shop_name").Value.ToString();
            userRole = snapshot.Child("user_privilege").Value.ToString();
            profileId = snapshot.Child("profile_id").Value.ToString();

            // Check if the user privilege is not user or else move to another scene
            if (userRole != "user")
            {
                firebaseAuthHandler.auth.SignOut(); // is the above condition is true force logout the user that tried to login
                accountHandler.popUpWindowLogin.SetActive(true);
                accountHandler.warningLogInText.text = "Oops you are not an authorized account";

            }
            else
            {
                SceneManager.LoadScene(1); // Moves to the seller home page
            }
        }
    }

    // Gets the profile picture id
    public IEnumerator GetProfilePicId()
    {
        // Get the Current Account in Auth
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;

        // Load the Account with the same ID in Auth
        var DBTask = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).GetValueAsync();

        // Wait Until the Values are retrieved
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            // If there is and error
            UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTask.Exception);
        }
        else if (DBTask.Result.Value == null)
        {
            // If there is no data Delete
            firebaseAuthHandler.User.DeleteAsync().ContinueWith(DBTask =>
            {
                if (DBTask.IsCanceled)
                {
                    //Canceled
                }
                if (DBTask.IsFaulted)
                {
                    //Faulted
                }

                Debug.Log("User Deleted");
            });
        }
        else
        {
            // Load the data
            DataSnapshot snapshot = DBTask.Result;

            profileId = snapshot.Child("profile_id").Value.ToString();// sets the profile id in the profile and display its equivalent
        }
    }
    #endregion

    #region UploadForm
    /// <summary>
    /// This function is responsible for upload of all datas in the database
    /// </summary>
    /// <param name="trackingNumber">Unique Id of the Product</param>
    /// <param name="buyerName">Buyers full name</param>
    /// <param name="amount">Total Amount of the product</param>
    /// <param name="status"> Status of the product</param>
    /// <param name="sellerName">Seller's full name</param>
    /// <param name="shopName">Shop's name</param>
    /// <param name="description">Short Description about the product</param>
    /// <param name="destination">Destination of the product</param>
    /// <param name="dateAndTime">Date when the order was made</param>
    /// <param name="eta">Estimated time of arrival</param>
    /// <param name="currentDa">Current Dropping area it is located</param>
    /// <param name="otherDetails">Optional</param>
    /// <param name="payment">Payment Status</param>
    public void UploadFormDataInDatabase(int trackingNumber, string buyerName, int amount, string status, string sellerName,
    string shopName, string description, string destination, string dateAndTime, string eta, string currentDa, string otherDetails,
    string length, string width, string height, string weight, string payment)
    {
        this.trackingNumber = trackingNumber;

        StartCoroutine(UploadToUserDatabase(trackingNumber));
        StartCoroutine(UploadTrackingNumberToDatabase(trackingNumber));
        StartCoroutine(UploadBuyerNameToDatabase(trackingNumber, buyerName));
        StartCoroutine(UploadAmountToDatabase(trackingNumber, amount));
        StartCoroutine(UploadDescriptionToDatabase(trackingNumber, description));
        StartCoroutine(UploadSellerNameToDatabase(trackingNumber, sellerName));
        StartCoroutine(UploadShopNameToDatabase(trackingNumber, shopName));
        StartCoroutine(UploadDestinationToDatabase(trackingNumber, destination));
        StartCoroutine(UploadDateToDatabase(trackingNumber, dateAndTime));
        StartCoroutine(UploadEtaToDatabase(trackingNumber, eta));
        StartCoroutine(UploadOtherDetailsToDatabase(trackingNumber, otherDetails));
        StartCoroutine(UploadLengthToDatabase(trackingNumber, length));
        StartCoroutine(UploadWidthToDatabase(trackingNumber, width));
        StartCoroutine(UploadHeightToDatabase(trackingNumber, height));
        StartCoroutine(UploadWeightToDatabase(trackingNumber, weight));
        StartCoroutine(SetCurrentDAInDatabase(trackingNumber.ToString(), currentDa));
        StartCoroutine(SetPackageStatusInDatabase(trackingNumber.ToString(), status));
        StartCoroutine(UploadPaymentToDatabase(trackingNumber, payment));
    }

    private IEnumerator UploadToUserDatabase(int trackingNumber)
    {
        firebaseAuthHandler.User = firebaseAuthHandler.auth.CurrentUser;
        var DBTask = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).Child("Tracking_Numbers").Child(trackingNumber.ToString()).SetValueAsync(trackingNumber);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadTrackingNumberToDatabase(int trackingNumber)
    {
        var DBTaskTrackingNumber = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Tracking_Number").SetValueAsync(trackingNumber);

        yield return new WaitUntil(predicate: () => DBTaskTrackingNumber.IsCompleted);

        if (DBTaskTrackingNumber.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    public IEnumerator UploadPaymentToDatabase(int trackingNumber, string payment)
    {
        var DBTaskTrackingNumber = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Payment").SetValueAsync(payment);

        yield return new WaitUntil(predicate: () => DBTaskTrackingNumber.IsCompleted);

        if (DBTaskTrackingNumber.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadBuyerNameToDatabase(int trackingNumber, string buyerName)
    {
        var DBTaskBuyerName = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Buyer_Name").SetValueAsync(buyerName);

        yield return new WaitUntil(predicate: () => DBTaskBuyerName.IsCompleted);

        if (DBTaskBuyerName.Exception != null)
        {
            //UnityEngine.Debug.Log(DBTaskBuyerName.Exception);
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadSellerNameToDatabase(int trackingNumber, string sellerName)
    {
        var DBTaskSellerName = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Seller_Name").SetValueAsync(sellerName);

        yield return new WaitUntil(predicate: () => DBTaskSellerName.IsCompleted);

        if (DBTaskSellerName.Exception != null)
        {
            //UnityEngine.Debug.Log(DBTaskBuyerName.Exception);
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadShopNameToDatabase(int trackingNumber, string shopName)
    {
        var DBTaskShopName = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Shop_Name").SetValueAsync(shopName);

        yield return new WaitUntil(predicate: () => DBTaskShopName.IsCompleted);

        if (DBTaskShopName.Exception != null)
        {
            //UnityEngine.Debug.Log(DBTaskBuyerName.Exception);
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadAmountToDatabase(int trackingNumber, int amount)
    {
        var DBTaskAmount = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Amount").SetValueAsync(amount);

        yield return new WaitUntil(predicate: () => DBTaskAmount.IsCompleted);

        if (DBTaskAmount.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadDescriptionToDatabase(int trackingNumber, string description)
    {
        var DBTaskDescription = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Description").SetValueAsync(description);

        yield return new WaitUntil(predicate: () => DBTaskDescription.IsCompleted);

        if (DBTaskDescription.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator SetCurrentDAInDatabase(string trackingNumber, string currentDA)
    {
        var DBTaskDA = DBreference.Child("TrackingNumbers").Child(trackingNumber).Child("Current_DA").SetValueAsync(currentDA);

        yield return new WaitUntil(predicate: () => DBTaskDA.IsCompleted);

        if (DBTaskDA.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadDestinationToDatabase(int trackingNumber, string destination)
    {
        var DBTaskDestination = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Destination").SetValueAsync(destination);

        yield return new WaitUntil(predicate: () => DBTaskDestination.IsCompleted);

        if (DBTaskDestination.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadDateToDatabase(int trackingNumber, string currentDate)
    {
        var DBTaskDate = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Drop_Date").SetValueAsync(currentDate);

        yield return new WaitUntil(predicate: () => DBTaskDate.IsCompleted);

        if (DBTaskDate.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator SetPackageStatusInDatabase(string trackingNumber, string status)
    {
        var DBTaskStatus = DBreference.Child("TrackingNumbers").Child(trackingNumber).Child("Status").SetValueAsync(status);

        yield return new WaitUntil(predicate: () => DBTaskStatus.IsCompleted);

        if (DBTaskStatus.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadEtaToDatabase(int trackingNumber, string eta)
    {
        var DBTaskEta = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("ETA").SetValueAsync(eta);

        yield return new WaitUntil(predicate: () => DBTaskEta.IsCompleted);

        if (DBTaskEta.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadOtherDetailsToDatabase(int trackingNumber, string otherDetails)
    {
        var DBTaskOtherDetails = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Other_Details").SetValueAsync(otherDetails);

        yield return new WaitUntil(predicate: () => DBTaskOtherDetails.IsCompleted);

        if (DBTaskOtherDetails.Exception != null)
        {
            //UnityEngine.Debug.Log(DBTaskBuyerName.Exception);
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadLengthToDatabase(int trackingNumber, string length)
    {
        var DBTaskLength = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Length").SetValueAsync(length);

        yield return new WaitUntil(predicate: () => DBTaskLength.IsCompleted);

        if (DBTaskLength.Exception != null)
        {
            //UnityEngine.Debug.Log(DBTaskBuyerName.Exception);
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadWidthToDatabase(int trackingNumber, string width)
    {
        var DBTaskWidth = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Width").SetValueAsync(width);

        yield return new WaitUntil(predicate: () => DBTaskWidth.IsCompleted);

        if (DBTaskWidth.Exception != null)
        {
            //UnityEngine.Debug.Log(DBTaskBuyerName.Exception);
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadHeightToDatabase(int trackingNumber, string height)
    {
        var DBTaskHeight = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Height").SetValueAsync(height);

        yield return new WaitUntil(predicate: () => DBTaskHeight.IsCompleted);

        if (DBTaskHeight.Exception != null)
        {
            //UnityEngine.Debug.Log(DBTaskBuyerName.Exception);
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator UploadWeightToDatabase(int trackingNumber, string weight)
    {
        var DBTaskWeight = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Weight").SetValueAsync(weight);

        yield return new WaitUntil(predicate: () => DBTaskWeight.IsCompleted);

        if (DBTaskWeight.Exception != null)
        {
            //UnityEngine.Debug.Log(DBTaskBuyerName.Exception);
        }
        else
        {
            //Update Username
        }
    }

    private IEnumerator GetPackageStatusInDatabase(int trackingNumber, string status)
    {
        var DBTaskStatus = DBreference.Child("TrackingNumbers").Child(trackingNumber.ToString()).Child("Status").SetValueAsync(status);

        yield return new WaitUntil(predicate: () => DBTaskStatus.IsCompleted);

        if (DBTaskStatus.Exception != null)
        {
            //Error
        }
        else
        {
            //Update Username
        }
    }

    /// <summary>
    /// Generates the Form Data that will be checked by the admin
    /// </summary>
    public void GenerateFormData()
    {
        buyerName = formCreator.buyerName.text;
        address = formCreator.address.text;
        otherDetails = formCreator.otherDetails.text;
        description = formCreator.description.text;

        clientPhoneNumber = int.Parse(formCreator.phoneNumber.text);
        length = int.Parse(formCreator.length.text);
        width = int.Parse(formCreator.width.text);
        height = int.Parse(formCreator.height.text);
        weight = int.Parse(formCreator.weight.text);
        totalAmount = int.Parse(formCreator.totalAmount.text);

    }

    // Generates the datas that are needed to be hard coded and calculated by the system
    public IEnumerator UploadFormData()
    {
        var DBTaskData = DBreference.Child("TrackingNumbers").GetValueAsync(); // Gets a reference from the TrackingNumbers Database

        System.DateTime dateAndTime = System.DateTime.Now; // Gets the time and date now

        string convertedDate = dateAndTime.Month + "/" + dateAndTime.Day + "/" + dateAndTime.Year; // Converts it to a string format

        string convertedEtaDate = dateAndTime.AddDays(5).Month + "/" + dateAndTime.AddDays(5).Day + "/" + dateAndTime.AddDays(5).Year; // Adds 5 days from the original date

        yield return new WaitUntil(predicate: () => DBTaskData.IsCompleted);

        if (DBTaskData.Exception != null)
        {
            // If there is and error
            UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTaskData.Exception);
        }
        else if (DBTaskData.Result.Value == null)
        {
            // Tracking Number Generator Logic
            int trackingNumberShuffler = UnityEngine.Random.Range(1000000, 9999999); // Produce a random number from min to max

            storageHandler.UploadFileInStorage(trackingNumberShuffler.ToString()); // Uploads the tracking number to the Storage with the picture included if any
            // Assigns all the data given by the form to the function and uploads to the database
            UploadFormDataInDatabase(trackingNumberShuffler, buyerName, totalAmount, "Pending",
            fullName, shopName, description, address, convertedDate, convertedEtaDate, "YT Pasabay",
            otherDetails, length.ToString(), width.ToString(), height.ToString(), weight.ToString(), "Unpaid");
        }
        else
        {
            // Load the data
            DataSnapshot snapshot = DBTaskData.Result;

            int trackingNumberShuffler = UnityEngine.Random.Range(1000000, 9999999); // Produce a random number from min to max
            // if the tracking number already exists in the database generate a new one
            while (snapshot.Child(trackingNumberShuffler.ToString()).Exists)
            {
                trackingNumberShuffler = UnityEngine.Random.Range(1000000, 9999999);
            }

            storageHandler.UploadFileInStorage(trackingNumberShuffler.ToString()); // Uploads the tracking number to the Storage with the picture included if any
            // Assigns all the data given by the form to the function and uploads to the database
            UploadFormDataInDatabase(trackingNumberShuffler, buyerName, totalAmount, "Pending",
            fullName, shopName, description, address, convertedDate, convertedEtaDate, "YT Pasabay",
            otherDetails, length.ToString(), width.ToString(), height.ToString(), weight.ToString(), "Unpaid");
        }
    }

    // Gets the form data and assigns it to a User Interface component
    public IEnumerator GetFormData(string status)
    {
        var DBTaskTrackingNumbers = DBreference.Child("users").Child(firebaseAuthHandler.User.UserId).Child("Tracking_Numbers").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTaskTrackingNumbers.IsCompleted);

        if (DBTaskTrackingNumbers.Exception != null)
        {
            // If there is and error
            UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTaskTrackingNumbers.Exception);
        }
        else if (DBTaskTrackingNumbers.Result.Value == null)
        {
            // If there is no data Send Error
            UnityEngine.Debug.Log("No Data");
        }
        else
        {
            // Load the data
            DataSnapshot snapshot = DBTaskTrackingNumbers.Result;

            int index = 0;

            foreach (var child in snapshot.Children)
            {
                var DBTaskData = DBreference.Child("TrackingNumbers").Child(child.Key).GetValueAsync();

                yield return new WaitUntil(predicate: () => DBTaskData.IsCompleted);

                if (DBTaskData.Exception != null)
                {
                    // If there is and error
                    UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTaskData.Exception);
                }
                else if (DBTaskData.Result.Value == null)
                {
                    // If there is no data Send Error
                    UnityEngine.Debug.Log("No Data");
                }
                else
                {
                    // Load the data
                    DataSnapshot DBTaskDataSnapshot = DBTaskData.Result;
                    // Gets only the data with the same status
                    if (DBTaskDataSnapshot.Child("Status").Value.ToString() == status)
                    {
                        // Assigns all values from database to the UI Components
                        packageMonitor.AssignValues(DBTaskDataSnapshot.Child("Shop_Name").Value.ToString(), DBTaskDataSnapshot.Child("Tracking_Number").Value.ToString(), DBTaskDataSnapshot.Child("Description").Value.ToString(), DBTaskDataSnapshot.Child("Amount").Value.ToString(), DBTaskDataSnapshot.Child("Drop_Date").Value.ToString(), DBTaskDataSnapshot.Child("ETA").Value.ToString(), DBTaskDataSnapshot.Child("Buyer_Name").Value.ToString(), DBTaskDataSnapshot.Child("Seller_Name").Value.ToString(), DBTaskDataSnapshot.Child("Destination").Value.ToString(), DBTaskDataSnapshot.Child("Status").Value.ToString(), DBTaskDataSnapshot.Child("Current_DA").Value.ToString(), index);
                        index++;
                    }

                }
            }
        }
    }
    #endregion

    #region TrackPackage
    // Get the data even if you have no account in the database but limited only for users that want to track their package
    public IEnumerator GetDataForUsers(string inputTrackingNumber)
    {
        var DBTaskTrackingNumbers = DBreference.Child("TrackingNumbers").Child(inputTrackingNumber).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTaskTrackingNumbers.IsCompleted);

        if (DBTaskTrackingNumbers.Exception != null)
        {
            // If there is and error
            UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTaskTrackingNumbers.Exception);
        }
        else if (DBTaskTrackingNumbers.Result.Value == null)
        {
            // If there is no data Send Error
            packageLocator.noData = true;
            packageLocator.EndExecution();
        }
        else
        {
            // Load the data
            DataSnapshot snapshot = DBTaskTrackingNumbers.Result;
            // Assigns the values to the form
            packageLocator.AssignValues(snapshot.Child("Shop_Name").Value.ToString(), snapshot.Child("Tracking_Number").Value.ToString(), snapshot.Child("Description").Value.ToString(), snapshot.Child("Amount").Value.ToString(), snapshot.Child("Drop_Date").Value.ToString(), snapshot.Child("Status").Value.ToString(), snapshot.Child("Current_DA").Value.ToString());
        }
    }

    // Gets the data for sellers data is not limited but you need an account
    public IEnumerator GetDataForSellers(string inputTrackingNumber)
    {
        var DBTaskTrackingNumbers = DBreference.Child("TrackingNumbers").Child(inputTrackingNumber).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTaskTrackingNumbers.IsCompleted);

        if (DBTaskTrackingNumbers.Exception != null)
        {
            // If there is and error
            UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTaskTrackingNumbers.Exception);
        }
        else if (DBTaskTrackingNumbers.Result.Value == null)
        {
            // If there is no data Send Error
            UnityEngine.Debug.Log("No Data");
        }
        else
        {
            // Load the data
            DataSnapshot snapshot = DBTaskTrackingNumbers.Result;

            packageMonitor.AssignValuesToForm(snapshot.Child("Shop_Name").Value.ToString(), snapshot.Child("Tracking_Number").Value.ToString(), snapshot.Child("Description").Value.ToString(), snapshot.Child("Amount").Value.ToString(), snapshot.Child("Drop_Date").Value.ToString(), snapshot.Child("ETA").Value.ToString(), snapshot.Child("Buyer_Name").Value.ToString(), snapshot.Child("Seller_Name").Value.ToString(), snapshot.Child("Destination").Value.ToString(), snapshot.Child("Status").Value.ToString(), snapshot.Child("Current_DA").Value.ToString());
        }
    }
    #endregion

    #region MessagingSystem

    // Gets a message
    public IEnumerator GetAMessage()
    {
        //userNameText.text = "Admin";

        var DBTask = DBreference.Child("UserMessages").Child(firebaseAuthHandler.User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            // If there is and error
            UnityEngine.Debug.LogWarning("Failed to run load Database" + DBTask.Exception);
        }
        else if (DBTask.Result.Value == null)
        {
            // If there is no data Send Error
            UnityEngine.Debug.Log("No Data");
        }
        else
        {
            // Load the data
            DataSnapshot snapshot = DBTask.Result;

            foreach (var messages in snapshot.Children)
            {
                messagesList.Add(messages.Child("message").Value.ToString());
                senderList.Add(messages.Child("sender").Value.ToString());
            }

            chatHandler.WriteMessage(messagesList, messagesList.Count, senderList);
        }
    }

    public void DeleteAMessage()
    {
        //DBReferenceN.ValueChanged -= HandleChildAdded;
        //DBReferenceN = null;
        messagesList.Clear();
    }

    // Sends a message and stores it in the database
    public IEnumerator SendAMessage(string message)
    {
        string uniqueKey = DBreference.Child("UserMessages").Child(firebaseAuthHandler.User.UserId).Push().Key; // Generated a unnique key
        databaseKey = uniqueKey;
        var DBTask = DBreference.Child("UserMessages").Child(firebaseAuthHandler.User.UserId).Child(uniqueKey).Child("message").SetValueAsync(message);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
        }
        else
        {
            StartCoroutine(SetSender(firebaseAuthHandler.User.UserId, databaseKey, message)); // Sets the Sender of the message
        }
    }

    // Sets the sender of the message and uploads it to the database
    public IEnumerator SetSender(string userID, string uniqueKey, string message)
    {
        var DBTask = DBreference.Child("UserMessages").Child(userID).Child(uniqueKey).Child("sender").SetValueAsync(fullName);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            //Error
        }
        else
        {

            chatHandler.WriteMessageListener(message, fullName);
            //Debug.Log("SetSender");
        }
    }

    /// <summary>Deprecated Code</summary>
    /*public void PostMessage()
    {
        DBReferenceN = FirebaseDatabase.DefaultInstance.GetReference("UserMessages/" + firebaseAuthHandler.User.UserId);
        DBReferenceN.ValueChanged += HandleChildAdded;
    }

    public void DeleteAMessage()
    {
        DBReferenceN.ValueChanged -= HandleChildAdded;
        DBReferenceN = null;
        messagesList.Clear();
    }

    void HandleChildAdded(object sender, ValueChangedEventArgs args)
    {
        if(args.DatabaseError != null)
        {
            UnityEngine.Debug.Log(args.DatabaseError.Message);
            return;
        }

        chatHandler.WriteMessageListener(args.Snapshot.Child(databaseKey).Value.ToString());
        //messagesList.Add(args.Snapshot.Child(databaseKey).Value.ToString());
    }*/


    #endregion
}

