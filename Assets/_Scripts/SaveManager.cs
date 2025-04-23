using OpenAI;
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
    private string savePath = null;
    private string imagePath = null;
    private string aiImagePath = null;


    private void Start()
    {
        GameObject cameraGameObject = GameObject.Find("/Cameras/AI-Camera");
        if (cameraGameObject == null)
        {
            Debug.Log($"[{this.gameObject.name}] AICamera Can Not Be Found!!");
        }
    }

    public void SaveImage()
    {
        if (theAICameraManager == null) return;

        savePath = Path.Combine(Application.persistentDataPath, subfolderName);
        theAICameraManager.SnapPhoto();
        byte[] bytes = theAICameraManager.capturedPhoto.EncodeToPNG();

        if (Directory.Exists(savePath))
        {
            WriteToPng(fileName, bytes);
        }
        else {
            CreateDirectory(subfolderName);
            WriteToPng(fileName, bytes);
        }
    }

    public void SaveAIPhoto(Texture2D aiPhoto, string fileName, string subfolderName = "AI-Edited-Images")
    {
        string photoPath = Path.Combine(Application.persistentDataPath, subfolderName);
        byte[] photobytes = aiPhoto.EncodeToPNG();

        if (Directory.Exists(photoPath))
        {
            WriteToPath(fileName, photoPath, photobytes);
        }
        else
        {
            CreateDirectory(subfolderName);
            WriteToPath(fileName, photoPath, photobytes);
        }
    }

    private void CreateDirectory(string subfolderName)
    {
        savePath = Path.Combine(Application.persistentDataPath, subfolderName);
        Directory.CreateDirectory(savePath);
    }

    private void WriteToPng(string fileName, byte[] bytes)
    {
        int fileCount = 1;
        string checkFilePath = Path.Combine(savePath, $"{fileName}.png");
        while (File.Exists(checkFilePath))
        {
            checkFilePath = Path.Combine(savePath, $"{fileName} ({fileCount}).png");
        }
        File.WriteAllBytes(checkFilePath, bytes);
        Debug.Log($"{fileName}.png Saved to: {checkFilePath}");
    }

    private void WriteToPath(string fileName, string filePath, byte[] bytes)
    {
        int fileCount = 1;
        string checkFilePath = Path.Combine(filePath, $"{fileName}.png");
        while (File.Exists(checkFilePath))
        {
            checkFilePath = Path.Combine(filePath, $"{fileName} ({fileCount}).png");
        }
        
        File.WriteAllBytes(checkFilePath, bytes);
        aiImagePath = checkFilePath;
        Debug.Log($"{fileName}.png Saved to: {checkFilePath}");
    }

    public String GetSavePath()
    {
        return savePath;
    }

    public String GetImagePath()
    {
        imagePath = Path.Combine(savePath, $"{fileName}.png");
        return imagePath;
    }

    public String GetAIImagePath()
    {
        return aiImagePath;
    }
}
