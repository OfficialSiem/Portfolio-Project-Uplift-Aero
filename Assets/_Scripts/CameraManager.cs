using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (Camera))]
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public Camera aiCamera = null;

    [SerializeField]
    public RenderTexture _mainrenderTexture = null;

    [SerializeField]
    public RenderTexture _AIrenderTexture = null;
    
    [SerializeField]
    public Texture2D _image = null;

    private void OnEnable()
    {
        if(aiCamera == null)
        {
            aiCamera = this.GetComponent<Camera>();
            Debug.Log("Found Camera");
        }
    }
    
    public void SnapPhoto()
    {
        
        //Save the original main camera's renderTexture
        _mainrenderTexture = RenderTexture.active;

        //Set the active render texgture to the one our AI-Camera will use
        RenderTexture.active = aiCamera.targetTexture;

        //Render the shot
        aiCamera.Render();

        //For debug sake, save the current render Texture
        _AIrenderTexture = RenderTexture.active;

        //Create a new Texture2D that matches our AI-Cameras dimensions
        _image = new Texture2D(aiCamera.targetTexture.width, aiCamera.targetTexture.height, TextureFormat.RGB24, true);

        //Read the pixels from the AI-Camera's RenderTexture 
        _image.ReadPixels(new Rect(0, 0, aiCamera.targetTexture.width, aiCamera.targetTexture.height), 0, 0);

        //Apply or save the Image
        _image.Apply();

        //Change the Active texture back to the main cameras
        RenderTexture.active = _mainrenderTexture;
        
    }

   
}
