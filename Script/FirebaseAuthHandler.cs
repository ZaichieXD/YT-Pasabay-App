using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class FirebaseAuthHandler : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus; // Is the Account Valid
    public FirebaseAuth auth; // User Authentication System
    public FirebaseUser User;  //Firebase Registered User

    [Header("Indestructible")]
    public AccountHandler accountHandler; // Account Handler Object
    public FirebaseDatabaseHandler firebaseDatabaseHandler; // Database Handler Object

    #region Initialization
    // This runs First 
    private void Awake()
    {
        DontDestroyOnLoad(this);
        // Checks the firebase dependencies present on your system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are Activate firebase
                InitializeFirebase();
            }
            else
            {
                //Debug.Log("Could not resolve all Firebase Dependencies" + dependencyStatus);
            }
        });
    }

    private void Start()
    {
        if (auth.CurrentUser != null)
        {
            // User is already signed in, no need to authenticate again
            StartCoroutine(firebaseDatabaseHandler.LoadUserData());
        }
    }

    // Start Firebase
    private void InitializeFirebase()
    {
        //Debug.Log("Setting Up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }
    #endregion

    // Log Out Button
    public void SignOut()
    {
        // Log Out
        auth.SignOut();
        SceneManager.LoadScene(0);

    }

    #region Authentication
    // Log In Button
    public void LogInButton()
    {
        // Calls the Log In Coroutine
        StartCoroutine(Login(accountHandler.emailLogInField.text, accountHandler.passwordLogInField.text));
    }

    public void RegisterButton()
    {
        // Calls the Register Coroutine
        StartCoroutine(Register(accountHandler.emailRegisterField.text, accountHandler.passwordRegisterField.text));
    }

    // Log In Logic
    private IEnumerator Login(string _email, string _password)
    {
        // Call the firebase auth and pass the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        // Wait until task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            // If There are errors run this
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            // Specific Log in Error Message
            string message = "Login Failed";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            accountHandler.popUpWindowLogin.SetActive(true);
            accountHandler.warningLogInText.text = message;
        }
        else
        {
            // User Logged In
            accountHandler.ClearLogInScreen();
            StartCoroutine(firebaseDatabaseHandler.LoadUserData());
        }
    }

    // Register Code
    private IEnumerator Register(string _email, string _password)
    {
        if (_email == "")
        {
            // Pop Up if there is an Error
            accountHandler.popUpWindowSignIn.SetActive(true);
            accountHandler.warningRegisterText.text = "Missing Email";
        }
        else
        {
            // Creates a User Account in the User Authentication Database
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                // If There are errors run this
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                // Specific Error Messages
                string message = "Register Failed";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                accountHandler.popUpWindowSignIn.SetActive(true);
                accountHandler.warningRegisterText.text = message;
            }
            else
            {
                // User Created an Account and Gets the Result
                User = RegisterTask.Result;

                if (User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = _email };
                    //Updates the userprofile on the firebase with the profile variable
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    // Wait Until Task Completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        accountHandler.popUpWindowSignIn.SetActive(true);
                        accountHandler.warningRegisterText.text = "Error Setting Username";
                    }
                    else
                    {
                        firebaseDatabaseHandler.SaveDataButton();
                    }

                }
            }
        }
    }
    #endregion

}
