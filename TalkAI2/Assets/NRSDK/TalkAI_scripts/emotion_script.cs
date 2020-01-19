using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class emotion_script : MonoBehaviour
{
    public Text emotion_text;
    private string state;
    private controlScript controlScript;

    void Awake()
    {
        controlScript = GameObject.FindObjectOfType<controlScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        emotion_text.text = "Emotion";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            Output_Emotion(Input.inputString);
        }

    }

    public void Output_Emotion(string var)
    {
        controlScript.EmotionReciever(var);

        if(var == "1")
        {
            emotion_text.text = "Happy! :D";
        }
        else if (var == "2")
        {
            emotion_text.text = "Sad :(";
        }
        else if (var == "3")
        {
            emotion_text.text = "Calm :} ";
        }
        else if (var == "4")
        {
            emotion_text.text = "Surprised :O";
        }
        else
        {
            emotion_text.text = "Neutral :|";
        }
    }
}
