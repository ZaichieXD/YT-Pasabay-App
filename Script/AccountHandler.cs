using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class AccountHandler : MonoBehaviour
{

    [Header("Login")]
    public TMP_InputField emailLogInField; // Email Login Field
    public TMP_InputField passwordLogInField; // Password Field
    public TMP_Text warningLogInText; // Warning Text
    public GameObject popUpWindowLogin; // Pop Up Window Login
    public GameObject Login; // Log In Window

    [Header("Sign In")]
    public TMP_InputField emailRegisterField; // Email Register Field
    public TMP_InputField passwordRegisterField; // Password Register Field
    public TMP_Text warningRegisterText; // Warning Register Text
    public GameObject popUpWindowSignIn; // Sign In Pop Up Window
    public GameObject signIn; // Sign In Window

    [Header("Create Profile")]

    public TMP_InputField fullName; // Full Name Field
    public TMP_InputField fullAddress; // Full Address Field
    public TMP_InputField phoneNumber; // Phone Number Field
    public TMP_InputField shopName; // Shop Name Field


    private bool isRegistering;
    public GameObject mainMenu;

    private void Start()
    {
        // Runs at the Start

        mainMenu.SetActive(true);
        Login.SetActive(false);
        signIn.SetActive(false);
        isRegistering = false;      
    }
    
    // Clears the log in Screen Fields
    public void ClearLogInScreen()
    {
        emailLogInField.text = "";
        passwordLogInField.text = "";
    }

    // Clears the Register Screen Fields
    public void ClearRegisterScreen()
    {
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Login.activeInHierarchy == true)
        {
            mainMenu.SetActive(true);
            Login.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && signIn.activeInHierarchy == true)
        {
            mainMenu.SetActive(true);
            signIn.SetActive(false);
        }
    }


    //Switch to Sign In Screen
    public void SignInOpen()
    {
        signIn.SetActive(true);
        mainMenu.SetActive(false);
        isRegistering = true;
    }
    // Switch to Log in Screen
    public void LogInOpen()
    {
        Login.SetActive(true);
        mainMenu.SetActive(false);
        isRegistering = false;
    }
    // Close the Pop up Screen
    public void CloseButton()
    {
        if(isRegistering == false)
        {
            popUpWindowLogin.SetActive(false);
        }
        else
        {
            popUpWindowSignIn.SetActive(false);
        }
    }

    public void LoadToLoginScreen()
    {
        mainMenu.SetActive(true);
        signIn.SetActive(false);
        Login.SetActive(false);
    }
}
