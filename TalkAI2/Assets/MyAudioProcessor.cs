using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Net.Http;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MyAudioProcessor : MonoBehaviour
{
    public Text statusText;
    public Text statusText2;

    private string deviceName;
    private static int lengthSec = 20;
    private static int frequency = 16000;
    private AudioClip audioClip;
    private int count = 0;
    private Boolean isUserStart = false;

    private controlScript controlScript;

    void Awake()
    {
        controlScript = GameObject.FindObjectOfType<controlScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device); 
        }
        this.deviceName = Microphone.devices[0];
    }

    public void User_Request_Change()
    {
        if (this.isUserStart == true)
        {
            User_Request_Stop();
        }
        else
        {
            User_Request_Start();
        }
    }

    public void User_Request_Start()
    {
        statusText.text = "Recording Now";
        this.isUserStart = true;
        this.deviceName = Microphone.devices[0];

        count++;
        this.audioClip = Microphone.Start(this.deviceName, false, lengthSec, frequency);
    }

    public void User_Request_Stop()
    {
        statusText.text = "Stopped Recording";
        this.isUserStart = false;
        Microphone.End(this.deviceName);
    }

    // Update is called once per frame
    void Update()
    {
        if (isUserStart == false)
        {
            return;
        }
        
        if (Microphone.IsRecording(this.deviceName))
        {
            //statusText.text = "Name: " + this.deviceName;
            return;
        }
        else
        {
            if (this.audioClip == null)
            {
                //statusText.text = "null";
                count++;
                audioClip = Microphone.Start(this.deviceName, false, lengthSec, frequency);
                return;
            }
            //statusText.text = "Processing";
            float[] data = new float[this.audioClip.samples * this.audioClip.channels];
            this.audioClip.GetData(data, 0);
            MemoryStream memoryStream = SavWav.Save(this.audioClip);
            byte[] bytes = memoryStream.GetBuffer();
            //statusText.text = "Here C";

            Task.Run(() => MainAsync(bytes));

            //statusText.text = "Sending: data: " + data.Length + ", bytes: " + bytes.Length;
            //data.Length = 661500;
            //bytes.Length ==1323044

            count++;
            audioClip = Microphone.Start(this.deviceName, false, lengthSec, frequency);
            return;
        }
    }

    async Task MainAsync(byte[] bytes)
    {
        using (var client = new HttpClient())
        {
            //client.BaseAddress = new Uri("");
            string url = "http://3.135.252.232:80/";

            ByteArrayContent byteArrayContent = new ByteArrayContent(bytes);
            //statusText.text = "SendingA: data: " + bytes.Length;

            var result = await client.PostAsync(url, byteArrayContent);
            //statusText.text = "result: " + result.StatusCode;
            if (result.IsSuccessStatusCode)
            {
                string predictedEmotion = await result.Content.ReadAsStringAsync();
                //statusText.text = "Emo: " + predictedEmotion + ". Count:" + count;

                if (predictedEmotion == "happy")
                {
                    statusText2.text = "happy";
                    statusText.text = "Very Interesting!";
                    
                }
                else if (predictedEmotion == "sad")
                {
                    statusText.text = "Aww";
                    statusText2.text = "Sad";
                }
                else if (predictedEmotion == "angry")
                {
                    statusText.text = "Oh no!";
                    statusText2.text = "Angry";
                }
                else if (predictedEmotion == "surprise")
                {
                    statusText.text = "Oh Wow!";
                    statusText2.text = "Surprise";
                }
                else if (predictedEmotion == "engaged")
                {
                    statusText.text = "I see";
                    statusText2.text = "Engaged";
                }
                else 
                {
                    statusText.text = "Hmm...";
                    statusText2.text = "Neutral";
                }

                //neutral, calm, happy, sad, surprised
                controlScript.EmotionReciever(predictedEmotion);
            }

        }
    }


}
