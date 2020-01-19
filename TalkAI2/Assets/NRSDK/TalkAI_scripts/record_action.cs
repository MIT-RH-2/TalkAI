using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class record_action : MonoBehaviour
{
    public Text record_btn;
    private bool record_state = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_Recording()
    {
        if(!record_state)
        {
            record_state = true;
            record_btn.text = "Stop Recording";
            Debug.Log("recording_start");
        }

        else
        {
            record_state = false;
            record_btn.text = "Start Recording";
            Debug.Log("recording_stopped");
        }
 
    }
}
