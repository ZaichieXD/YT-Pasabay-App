using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Drawing;
using UnityEngine.SceneManagement;

public class FormCreator : MonoBehaviour
{
    public TMP_InputField buyerName;
    public TMP_InputField phoneNumber;
    public TMP_InputField address;
    public TMP_InputField otherDetails;

    public TMP_InputField length;
    public TMP_InputField width;
    public TMP_InputField height;
    public TMP_InputField weight;

    public TMP_InputField description;
    public TMP_InputField totalAmount;

    public TMP_Dropdown paymentMethod;

    public FirebaseDatabaseHandler databaseHandler;
    public FirebaseStorageHandler storageHandler;
    public IAPManager iapManager;
    public GameObject buyerForm;
    public GameObject paymentGateway;
    public GameObject shipSuccess;

    public TMP_InputField textInput;
    public RawImage tempImage;

    private Texture2D tex;

    public RawImage attachmentImage;

    public GameObject popupWindowError;
    public GameObject errorCloseButton;

    public void Start()
    {
        //fillUpForms.Add(formTemplate);

        tex = new Texture2D(256, 256);

        databaseHandler = GameObject.FindGameObjectWithTag("Database").GetComponent<FirebaseDatabaseHandler>();
        storageHandler = GameObject.FindGameObjectWithTag("Storage").GetComponent<FirebaseStorageHandler>();

        databaseHandler.FindFormCreator();
    }

    /// <summary>
    /// Button reference for the Form Upload
    /// </summary>
    public void UploadForm()
    {
        if (buyerName.text == null && phoneNumber.text == null && address.text == null)
        {
            if (length.text == null && width.text == null && height.text == null && weight.text == null)
            {
                if (totalAmount.text == null)
                {
                    SaveItemData();
                }
                else
                {
                    Debug.Log(message: "Required");
                    // Put required here
                }
            }
            else
            {
                Debug.Log("Required");
                // Put required here
            }
        }
        else
        {
            Debug.Log("Required");
            // Put required here
        }
    }



    public void GoToHome()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToViewList()
    {
        SceneManager.LoadScene(3);
    }
    public void SaveItemData()
    {
        databaseHandler.GenerateFormData();
        iapManager.GetTotalWeight();
        StartCoroutine(databaseHandler.UploadFormData());

        buyerForm.SetActive(false);
        paymentGateway.SetActive(true);
    }

    public void LoadForm()
    {
        shipSuccess.SetActive(true);
        paymentGateway.SetActive(value: false);
    }

    public void IncompleteForm()
    {
        popupWindowError.SetActive(true);
        errorCloseButton.SetActive(true);
    }

    public void CloseErrorWindow()
    {
        popupWindowError.SetActive(false);
        errorCloseButton.SetActive(false);
    }

    public void CloseWindow()
    {
        SceneManager.LoadScene(1);
    }

    public void AddImage()
    {
        storageHandler.PickImage(512, attachmentImage);
    }

    /*private Color32[] Encode(string textForEncoding, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };

        return writer.Write(textForEncoding);
    }

    public void GenerateQrCode()
    {
        EncodeTextToQR();
    }

    private void EncodeTextToQR()
    {
        string textWrite = string.IsNullOrEmpty(textInput.text) ? "You should write something" : textInput.text;
        Color32 [] convertPixelToTexture = Encode(textWrite, tex.width, tex.height);
        tex.SetPixels32(convertPixelToTexture);
        tex.Apply();

        tempImage.texture = tex;
    }*/



    /*public void AddForms()
    {
        GameObject tempObj = Instantiate(formTemplate, formParent, false);

        fillUpForms.Add(tempObj);
    }

    public void RemoveForms()
    {
        GameObject buttonPressed = EventSystem.current.currentSelectedGameObject;
        fillUpForms.RemoveAt(fillUpForms.IndexOf(buttonPressed.transform.parent.gameObject));
        Destroy(buttonPressed.transform.parent.gameObject);
    }*/



    /*public void FinalizeForm(int trackingNumber, int index)
    {
        TMP_InputField[] childData = fillUpForms[index].GetComponentsInChildren<TMP_InputField>();

        if(childData[0].text != null)
        {
            buyerName = childData[0].text;
        }
        else
        {
            UnityEngine.Debug.LogError("No Data");
        }

        if(childData[1].text != null)
        {
            amount = childData[1].text;
        }
        else
        {
            UnityEngine.Debug.LogError("No Data");
        }

        UserData userData = new UserData();

        userData.trackingNumber = trackingNumber;
        userData.buyerName = this.buyerName;
        userData.amount = this.amount;

        storageHandler.UploadFileInStorage(userData);
    }*/


}