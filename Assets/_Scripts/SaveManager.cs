using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private CameraManager theAICameraManager;
    public string fileName = "UnityPic";
    public string subfolderName = "Snapshots";
    public int fileCounter = 0;
    private string savePath = null;

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, subfolderName);
        GameObject cameraGameObject = GameObject.Find("/Cameras/AI-Camera");
        if (cameraGameObject != null)
        {
            //theAICameraManager = cameraGameObject.GetComponent<CameraManager>();
            //Debug.Log("Found Camera Manager");
        }
    }

    public void SaveImage()
    {
        if (theAICameraManager == null) return;
       
        theAICameraManager.SnapPhoto();
        byte[] bytes = theAICameraManager._image.EncodeToPNG();

        if (fileCounter > 0) fileName = fileName + $" ({fileCounter})";

        if(File.Exists(savePath))
        {
            WriteToPng(fileName, bytes);
        }
        else {
            CreateDirectory(subfolderName);
            WriteToPng(fileName, bytes);
        }

        fileCounter++;
    }

    private void CreateDirectory(string subfolderName)
    {
        savePath = Path.Combine(Application.persistentDataPath, subfolderName);
        Directory.CreateDirectory(savePath);
    }

    private void WriteToPng(string fileName, byte[] bytes)
    {
        File.WriteAllBytes(savePath + $"{fileName}" + ".png", bytes);
        Debug.Log($"{fileName}.png Saved to: {savePath}");
    }
}
