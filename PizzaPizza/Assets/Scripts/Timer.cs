using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Tooltip("The normal font color for the timer text")]
    public Color normalFontColor;
    [Tooltip("the font color for when you are out of time")]
    public Color outOfTimeFontColor;
    [Tooltip("the text for the timer")]
    public TextMeshProUGUI timerText;

    public void UpdateTimer(float time, float totalTime)
    {
        Debug.Log("update timer");
        int seconds = (int)(totalTime - time);
        if(seconds < 0)
        {
            timerText.color = outOfTimeFontColor;
        }
        else
        {
            timerText.color = normalFontColor;
        }
        int minutes = seconds / 60;
        seconds = Mathf.Abs(seconds % 60);
        string textStuff = ":";
        if(seconds < 10)
        {
            textStuff = ":0";
        }
        timerText.text = minutes + textStuff + seconds;
    }
}
