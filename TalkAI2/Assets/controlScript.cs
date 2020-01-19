using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlScript : MonoBehaviour
{
    public Text reaction_text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EmotionReciever(string var)
    {
        if (var == "happy")
        {
            reaction_text.text = "Very Interesting!";
            HappyCalled();
        }
        else if (var == "sad")
        {
            reaction_text.text = "Aww";
            SadCalled();
        }
        else if (var == "angry")
        {
            reaction_text.text = "Oh no!";
            AngryCalled();
        }
        else if (var == "surprised")
        {
            reaction_text.text = "Oh Wow!";
            SurprisedCalled();
        }
        else
        {
            reaction_text.text = "Hmm...";
            NeutralCalled();
        }
    }

    void HappyCalled()
    {
        GetComponent<Animator>().SetTrigger("Happy_flag");
    }
    void SadCalled()
    {
        GetComponent<Animator>().SetTrigger("Sad_flag");
    }
    void AngryCalled()
    {
        GetComponent<Animator>().SetTrigger("Anger_flag");
    }
    void SurprisedCalled()
    {
        GetComponent<Animator>().SetTrigger("Surprised_flag");
    }
    void NeutralCalled()
    {
        GetComponent<Animator>().SetTrigger("Calm_nod_flag");
    }
}
