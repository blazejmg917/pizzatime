using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [Tooltip("money text")]
    public TextMeshProUGUI moneyText;
    public float money;
    [Tooltip("time text")]
    public TextMeshProUGUI timeText;
    public float time;
    [Tooltip("pizza text")]
    public TextMeshProUGUI pizzaText;
    public int pizza;
    

    void Start()
    {
        ClearDisplay();
    }

    public void ClearDisplay()
    {
        money = 0f;
        time = 0f;
        pizza = 0;
        moneyText.text = "$0";
        pizzaText.text = "0 Pizzas";
        timeText.text = "0:00";

    }

    // Start is called before the first frame update
    public void UpdateDisplay(float newMoney)
    {
        money += newMoney;
        pizza++;
        if (pizza == 1)
        {
            pizzaText.text = pizza + " Pizza";
        }
        else
        {
            pizzaText.text = pizza + " Pizzas";
        }
            
        money = Mathf.Round(money * 100) / 100;
        moneyText.text = "$" + money;

    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        int seconds = (int)time;
        int minutes = seconds / 60;
        seconds %= 60;
        int hours = minutes / 60;
        minutes %= 60;
        string displayText = "";
        if(hours > 0)
        {
            displayText += hours + ":";
            if(minutes < 10)
            {
                displayText += "0";
            }
        }
        string textStuff = ":";
        if (seconds < 10)
        {
            textStuff = ":0";
        }
        displayText += minutes + textStuff + seconds;
        timeText.text = displayText;
    }

}
