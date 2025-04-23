using OpenAI;
using OpenAI.Images;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

public class OpenAIManager : MonoBehaviour
{
    #region OPENAI Variables
    [Tooltip("This is the API to help you access OpenAI.")]
    public OpenAIClient currentAPI = null;

    [Tooltip("This is the prompt our Image will generate from.")]
    public string prompt = "Amidst the remnants of an ancient temple intricately carved stone altars stand sentinel over a lush spacious display";

    [Tooltip("In a Debug-Inspector can use this field to see if something is generating")]
    public IReadOnlyList<ImageResult> listOfresults = new List<ImageResult>();
    #endregion

    #region Save Variables
    [Tooltip("We need the Save Manager to Read/Write on to disk")]
    public SaveManager saveManager = null;

    [Tooltip("What name you'd like the file to be")]
    public string saveFileName;
    #endregion

    public void GenerateNewImage()
    {
        //If we don't have a SaveManager, dont bother executing anything else
        if (saveManager == null)
            return;

        //Set up a CompletedTaskSource
        var currentTask = new TaskCompletionSource<Task>();
        
        //Run our OpenAITask
        Task.Run(async () =>
        {
            Debug.Log("Generation has started");
            await StartOpenAITask();

            //Set the completed state of our OpenAITask to the CompletedTaskSource from earlier 
            currentTask.SetResult(StartOpenAITask());
            Debug.Log("Generation has stopped");
        });

        //If Our OpenAITask did Complete its work, say so!
        currentTask.Task.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
        {
            Debug.Log("Generation completed");
        });
    }

    private async Task StartOpenAITask()
    {
        //Sometimes there is a weird bug for Texture2D in async method, just initalize the texture before assigning it
        Texture2D finalImage = new Texture2D(1,1);

        //Grab where our image is located
        string filepath = saveManager.GetImagePath();

        //Then start a new instance of our OpenAI API
        currentAPI = new OpenAIClient(new OpenAIAuthentication().LoadFromEnvironment());

        //OpenAI has some request requirements for editing images
        var request = new ImageEditRequest(filepath, prompt, size: ImageSize.Large);

        //Save the array of responses we get back from OpenAI
        listOfresults = await currentAPI.ImagesEndPoint.CreateImageEditAsync(request);

        //Foreach response we get back
        foreach (var result in listOfresults)
        {
            Debug.Log(result.ToString());
            //grab the texture
            finalImage = result.Texture;

            //and if its not null go ahead and save the Texture as a png
            if(finalImage != null)
            saveManager.SaveAIPhoto(finalImage, saveFileName);
            
        }

    }
}
