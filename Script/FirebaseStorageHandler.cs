
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Storage;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Extensions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class FirebaseStorageHandler : MonoBehaviour
{
    public string imagePath;

    FirebaseStorage storage;
    StorageReference storageReference;

    FirebaseDatabaseHandler databaseHandler;
    FormCreator formCreator;

    bool isExisting;

    // Gets the firebase storage bucket
    void Start()
    {
        DontDestroyOnLoad(this);

        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://yt-pasabay.appspot.com");
        databaseHandler = GameObject.FindGameObjectWithTag("Database").GetComponent<FirebaseDatabaseHandler>();
    }

    // Downloads the picture and assigns it to the image
    public void DownloadDataFromStorage(string trackingNumber, RawImage image)
    {
        StorageReference uploadref = storageReference.Child("TrackingNumbers/" + trackingNumber + ".jpg");
        const long maxAllowedSize = 1 * 8192 * 8192;
        uploadref.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread((Task<byte[]> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                UnityEngine.Debug.Log(task.Exception);
            }
            else
            {
                byte[] fileContents = task.Result;
                Texture2D textureFile = new Texture2D(512, 512);
                textureFile.LoadImage(fileContents);
                image.texture = textureFile;
            }
        });
    }

    // Uploads the image to the firebase storage
    public void UploadFileInStorage(string trackingNumber)
    {
        StorageReference uploadref = storageReference.Child("TrackingNumbers/" + trackingNumber + ".jpg");
        var newMetadata = new MetadataChange();
        newMetadata.ContentType = "image/jpeg";

        uploadref.PutFileAsync(imagePath, newMetadata).ContinueWithOnMainThread((task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                UnityEngine.Debug.Log("Failed");
            }
            else
            {
                UnityEngine.Debug.Log("Success");
            }
        });
    }

    // File Dialog for uploading pictures
    public void PickImage(int maxSize, RawImage imageButton)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                imagePath = "file://" + path;

                // Assign texture to a temporary quad and destroy it after 5 seconds
                // GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                // quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                // quad.transform.forward = Camera.main.transform.forward;
                // quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                // Material material = quad.GetComponent<Renderer>().material;
                // if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                //     material.shader = Shader.Find("Legacy Shaders/Diffuse");

                imageButton.texture = texture;
            }
        });

        Debug.Log("Permission result: " + permission);
    }
}
